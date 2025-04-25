using Application.DTOs;
using Application.Features.Auth.Commands;
using Application.Interfaces.Authentications;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, DTOResponse<string>>
    {
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        private readonly IRoleService _roleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordProvider _passwordProvider;
        private readonly ILogger<RegisterCommand> _logger; //ghi log theo name instance
        public RegisterCommandHandler(IUserService userService, IEmployeeService employeeService, IRoleService roleService, IUnitOfWork unitOfWork, IJwtProvider jwtProvider, IPasswordProvider passwordProvider, ILogger<RegisterCommand> logger)
        {
            _userService = userService;
            _employeeService = employeeService;
            _roleService = roleService;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _passwordProvider = passwordProvider;
            _logger = logger;
        }

        public async Task<DTOResponse<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _userService.CheckUserNameAsync(request.UserName))
                {
                    return new DTOResponse<string>()
                    {
                        IsError = true,
                        ErrorType = "2",
                        MessageError = $"Tên tài khoản {request.UserName} đã được sử dụng.",
                    };
                }

                if (await _userService.CheckEmailAsync(request.Email))
                {
                    return new DTOResponse<string>()
                    {
                        IsError = true,
                        ErrorType = "2",
                        MessageError = $"Địa chỉ Email {request.Email} đã được sử dụng.",
                    };
                }

                var hashPassword = _passwordProvider.HashPassword(request, request.Password);
                var roleEmp = await _roleService.GetByRoleName("User");
                if (roleEmp == null)
                {
                    _logger.LogError("Tạo tài khoản không thành công. Không tìm thấy thông tin RoleName = User.");
                    return new DTOResponse<string>()
                    {
                        IsError = true,
                        ErrorType = "3",
                        MessageError = "Tạo tài khoản không thành công. Vui lòng thử lại hoặc liên hệ nhà phát triển",
                    };
                }

                var employee = new Employees()
                {
                    RecID = Guid.NewGuid(),
                    EmployeeID = _employeeService.GenerateEmployeeID(),
                    EmployeeName = request.FullName,
                    Gender = request.Gender,
                    Birthday = request.Birthday,
                    Email = request.Email,
                    Phone = request.Phone,
                    Address = request.Address,
                    CreatedBy = request.UserName,
                    CreatedOn = DateTime.Now,
                };
                var user = new Users()
                {
                    RecID = Guid.NewGuid(),
                    UserName = request.UserName,
                    Password = hashPassword,
                    EmployeeID = employee.EmployeeID,
                    RoleID = roleEmp.RecID.ToString(),
                    Status = "1",
                    RefreshToken = _jwtProvider.GenerateRefreshToken(),
                    CreatedBy = request.UserName,
                    CreatedOn = DateTime.Now,
                };
                var dtoUser = new DTOUser()
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Address = request.Address,
                    Birthday = request.Birthday,
                    FullName = request.FullName,
                    Gender = request.Gender,
                    Role = roleEmp.RoleName
                };
                _userService.Add(user);
                _employeeService.Add(employee);
                await _unitOfWork.SaveAsync();

                var data = new { Token = _jwtProvider.GenerateToken(dtoUser), RefreshToken = user.RefreshToken };
                return new DTOResponse<string>()
                {
                    IsError = false,
                    Data = JsonSerializer.Serialize(data)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<string>()
                {
                    IsError = true,
                    ErrorType = "3",
                    MessageError = "Tạo tài khoản không thành công. Vui lòng thử lại hoặc liên hệ nhà phát triển."
                };
            }
        }
    }
}

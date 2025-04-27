using Application.DTOs;
using Application.Interfaces.Authentications;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using Azure.Core;
using Domain.Entities;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordProvider _passwordProvider;
        private readonly IRepository<Users> _userRepository;
        private readonly IRepository<Employees> _employeeRepository;
        private readonly IRepository<Roles> _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSetting _jwtSettings;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IRepository<Users> userRepository, IRepository<Employees> employeeRepository, IRepository<Roles> roleRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider, IOptions<JwtSetting> jwtSettings, IPasswordProvider passwordProvider, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _jwtSettings = jwtSettings.Value;
            _passwordProvider = passwordProvider;
            _logger = logger;
        }

        public async Task<DTOResponse<DTOToken>> Login(string username, string password)
        {
            try
            {
                var user = await _userRepository.GetOneAsync(x => x.UserName == username);
                if (user == null)
                {
                    return new DTOResponse<DTOToken>()
                    {
                        IsError = true,
                        ErrorType = "1",
                        MessageError = "Tên đăng nhập hoặc mật khẩu chưa chính xác."
                    };
                };

                var validPassword = _passwordProvider.VerifyPassword(user, user.Password, password);
                if (!validPassword)
                {
                    return new DTOResponse<DTOToken>()
                    {
                        IsError = true,
                        ErrorType = "1",
                        MessageError = "Tên đăng nhập hoặc mật khẩu chưa chính xác."
                    };
                }

                var dtoUser = new DTOUser()
                {
                    UserName = user.UserName,
                };
                Guid.TryParse(user.RoleID, out var gRoleID);
                var role = await _roleRepository.GetOneAsync(x => x.RecID == gRoleID);
                if (role != null)
                {
                    dtoUser.Role = role.RoleName;
                }
                var employee = await _employeeRepository.GetOneAsync(x => x.EmployeeID == user.EmployeeID);
                if (employee != null)
                {
                    dtoUser.FullName = employee.EmployeeName;
                    dtoUser.Gender = employee.Gender;
                    dtoUser.Birthday = employee.Birthday;
                    dtoUser.Phone = employee.Phone;
                    dtoUser.Email = employee.Email;
                    dtoUser.Address = employee.Address;
                }

                var dtoToken = new DTOToken() { AccessToken = _jwtProvider.GenerateToken(dtoUser), RefreshToken = _jwtProvider.GenerateRefreshToken() };
                user.RefreshToken = dtoToken.RefreshToken;
                user.RefreshTokenExperies = DateTime.Now.AddDays(_jwtSettings.RefreshTokenDays);
                return new DTOResponse<DTOToken>()
                {
                    IsError = false,
                    Data = dtoToken
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<DTOToken>()
                {
                    IsError = true,
                    ErrorType = "2",
                    MessageError = "Đăng nhập không thành công. Vui lòng thử lại hoặc liên hệ nhà phát triển."
                };
            }
        }

        public async Task<DTOResponse<DTOToken>> RefreshToken(string userName, string refreshToken)
        {
            try
            {
                var user = await _userRepository.GetOneAsync(x => x.UserName == userName && x.RefreshToken == refreshToken);
                if (user == null)
                {
                    return new DTOResponse<DTOToken>()
                    {
                        IsError = true,
                        ErrorType = "1",
                        MessageError = "Không tìm thấy thông tin người dùng.",
                    };
                }

                var dtoUser = new DTOUser()
                {
                    UserName = user.UserName,
                };
                Guid.TryParse(user.RoleID, out var gRoleID);
                var role = await _roleRepository.GetOneAsync(x => x.RecID == gRoleID);
                if (role != null)
                {
                    dtoUser.Role = role.RoleName;
                }

                var employee = await _employeeRepository.GetOneAsync(x => x.EmployeeID == user.EmployeeID);
                if (employee != null)
                {
                    dtoUser.FullName = employee.EmployeeName;
                    dtoUser.Gender = employee.Gender;
                    dtoUser.Birthday = employee.Birthday;
                    dtoUser.Phone = employee.Phone;
                    dtoUser.Email = employee.Email;
                    dtoUser.Address = employee.Address;
                }

                var dtoToken = new DTOToken()
                {
                    AccessToken = _jwtProvider.GenerateToken(dtoUser)
                };
                if (user.RefreshTokenExperies == null || user.RefreshTokenExperies < DateTime.Now)
                {
                    user.RefreshToken = _jwtProvider.GenerateRefreshToken();
                    user.RefreshTokenExperies = DateTime.Now.AddDays(_jwtSettings.RefreshTokenDays);
                    _userRepository.Update(user);
                }
                dtoToken.RefreshToken = user.RefreshToken;
                return new DTOResponse<DTOToken>()
                {
                    IsError = false,
                    Data = dtoToken,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<DTOToken>()
                {
                    IsError = true,
                    ErrorType = "2",
                    MessageError = "Hệ thống thực thi không thành công. Vui lòng thử lại hoặc liên hệ nhà phát triển."
                };
            }
        }

        public async Task<DTOResponse<DTOToken>> Register(DTORegister data)
        {
            try
            {
                var checkUserName = await _userRepository.ExistAsync(x => x.UserName == data.UserName);
                if (checkUserName)
                {
                    return new DTOResponse<DTOToken>()
                    {
                        IsError = true,
                        ErrorType = "1",
                        MessageError = $"Tên tài khoản {data.UserName} đã được sử dụng.",
                    };
                }

                var checkEmail = await _employeeRepository.ExistAsync(x => x.Email == data.Email);
                if (checkEmail)
                {
                    return new DTOResponse<DTOToken>()
                    {
                        IsError = true,
                        ErrorType = "1",
                        MessageError = $"Địa chỉ Email {data.Email} đã được sử dụng.",
                    };
                }

                var hashPassword = _passwordProvider.HashPassword(data, data.Password);
                var roleEmp = await _roleRepository.GetOneAsync(x => x.RoleName == "Employee");
                if (roleEmp == null)
                {
                    _logger.LogError("Tạo tài khoản không thành công. Không tìm thấy thông tin RoleName = Employee.");
                    return new DTOResponse<DTOToken>()
                    {
                        IsError = true,
                        ErrorType = "2",
                        MessageError = "Tạo tài khoản không thành công. Vui lòng thử lại hoặc liên hệ nhà phát triển",
                    };
                }
                var employee = new Employees()
                {
                    RecID = Guid.NewGuid(),
                    EmployeeID = GenerateEmployeeID(),
                    EmployeeName = data.FullName,
                    Gender = data.Gender,
                    Birthday = data.Birthday,
                    Email = data.Email,
                    Phone = data.Phone,
                    Address = data.Address,
                    CreatedBy = data.UserName,
                    CreatedOn = DateTime.Now,
                };
                var user = new Users()
                {
                    RecID = Guid.NewGuid(),
                    UserName = data.UserName,
                    Password = hashPassword,
                    EmployeeID = employee.EmployeeID,
                    RoleID = roleEmp.RecID.ToString(),
                    Status = "1",
                    RefreshToken = _jwtProvider.GenerateRefreshToken(),
                    RefreshTokenExperies = DateTime.Now.AddDays(_jwtSettings.RefreshTokenDays),
                    CreatedBy = data.UserName,
                    CreatedOn = DateTime.Now
                };
                _userRepository.Add(user);
                _employeeRepository.Add(employee);

                var dtoUser = new DTOUser()
                {
                    UserName = data.UserName,
                    Email = data.Email,
                    Phone = data.Phone,
                    Address = data.Address,
                    Birthday = data.Birthday,
                    FullName = data.FullName,
                    Gender = data.Gender,
                    Role = roleEmp.RoleName
                };
                var dtoToken = new DTOToken() { AccessToken = _jwtProvider.GenerateToken(dtoUser), RefreshToken = user.RefreshToken };
                return new DTOResponse<DTOToken>()
                {
                    IsError = false,
                    Data = dtoToken
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<DTOToken>()
                {
                    IsError = true,
                    ErrorType = "2",
                    MessageError = "Tạo tài khoản không thành công. Vui lòng thử lại hoặc liên hệ nhà phát triển."
                };
            }
        }

        private string GenerateEmployeeID()
        {
            var id = "";
            string day, month, year, hr, min, sec;
            DateTime date = DateTime.Now;
            day = date.Date.Day.ToString();
            month = date.Month.ToString();
            year = date.Year.ToString();
            hr = date.Hour.ToString();
            min = date.Minute.ToString();
            sec = date.Second.ToString();
            id = "E-" + day + month + year + hr + min + sec;
            return id;
        }
    }
}

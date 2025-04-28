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
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegisterCommand> _logger;
        public RegisterCommandHandler(IAuthService authService, IUnitOfWork unitOfWork, ILogger<RegisterCommand> logger)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<DTOResponse<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dtoRegister = new DTORegister()
                {
                    RecID = Guid.NewGuid(),
                    FullName = request.FullName,
                    Birthday = request.Birthday,
                    Gender = request.Gender,
                    Email = request.Email,
                    Phone = request.Phone,
                    UserName = request.UserName,
                    Password = request.Password
                };
                var result = await _authService.Register(dtoRegister);
                if (!result.IsError)
                {
                    await _unitOfWork.SaveAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<string>()
                {
                    IsError = true,
                    ErrorType = "2",
                    MessageError = "Tạo tài khoản không thành công. Vui lòng thử lại hoặc liên hệ nhà phát triển."
                };
            }
        }
    }
}

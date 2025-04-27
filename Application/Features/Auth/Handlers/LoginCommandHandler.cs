using Application.DTOs;
using Application.Features.Auth.Commands;
using Application.Interfaces.Authentications;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Features.Auth.Handlers
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, DTOResponse<DTOToken>>
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LoginCommandHandler> _logger;
        public LoginCommandHandler(IAuthService authService, IUnitOfWork unitOfWork, ILogger<LoginCommandHandler> logger)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTOResponse<DTOToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authService.Login(request.UserName, request.Password);
                if (!result.IsError)
                {
                    await _unitOfWork.SaveAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<DTOToken>()
                {
                    IsError = true,
                    ErrorType = "3",
                    MessageError = "Đăng nhập không thành công, vui lòng thử lại."
                };
            }
        }
    }
}

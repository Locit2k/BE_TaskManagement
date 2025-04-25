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
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, DTOResponse<string>>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordProvider _passwordProvider;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LoginCommandHandler> _logger;
        public LoginCommandHandler(IJwtProvider jwtProvider, IPasswordProvider paswordProvider, IUserService userService, IUnitOfWork unitOfWork, ILogger<LoginCommandHandler> logger)
        {
            _jwtProvider = jwtProvider;
            _passwordProvider = paswordProvider;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTOResponse<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dtoUser = await _userService.GetUserLogin(request.UserName, request.Password);
                if (dtoUser == null)
                {
                    return new DTOResponse<string>()
                    {
                        IsError = true,
                        ErrorType = "2",
                        MessageError = "Tên đăng nhập hoặc mật khẩu không chính xác."
                    };
                }

                var data = new { AccessToken = _jwtProvider.GenerateToken(dtoUser), RefreshToken = _jwtProvider.GenerateRefreshToken() };
                await _userService.UpdateRefreshToken(dtoUser.UserName, data.RefreshToken);
                await _unitOfWork.SaveAsync();
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
                    MessageError = "Đăng nhập không thành công, vui lòng thử lại."
                };
            }
        }
    }
}

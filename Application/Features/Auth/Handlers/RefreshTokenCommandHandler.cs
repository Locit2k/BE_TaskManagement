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
using System.Threading.Tasks;

namespace Application.Features.Auth.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, DTOResponse<string>>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<RefreshTokenCommandHandler> _logger;
        public RefreshTokenCommandHandler(IJwtProvider jwtProvider, IUserService userService, IUnitOfWork unitOfWork, ILogger<RefreshTokenCommandHandler> logger)
        {
            _jwtProvider = jwtProvider;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTOResponse<string>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dtoTokenUser = await _userService.GetUserByRefreshToken(request.UserName, request.RefreshToken);
                if (dtoTokenUser == null)
                {
                    return new DTOResponse<string>()
                    {
                        IsError = true,
                        ErrorType = "1",
                        MessageError = "RefreshToken không hợp lệ."
                    };
                }

                if (dtoTokenUser.RefreshTokenExperies.HasValue && dtoTokenUser.RefreshTokenExperies > DateTime.Now)
                {
                    var data = new { AccessToken = _jwtProvider.GenerateToken(dtoTokenUser), dtoTokenUser.RefreshToken };
                    return new DTOResponse<string>()
                    {
                        IsError = false,
                        Data = JsonSerializer.Serialize(data)
                    };
                }
                else
                {
                    var data = new { AccessToken = _jwtProvider.GenerateToken(dtoTokenUser), RefreshToken = _jwtProvider.GenerateRefreshToken() };
                    await _userService.UpdateRefreshToken(dtoTokenUser.UserName, data.RefreshToken);
                    await _unitOfWork.SaveAsync();
                    return new DTOResponse<string>()
                    {
                        IsError = false,
                        Data = JsonSerializer.Serialize(data)
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<string>()
                {
                    IsError = true,
                    ErrorType = "3",
                    MessageError = "Hệ thống thực thi không thành công. Vui lòng thử lại hoặc liên hệ nhà phát triển."
                };
            }
        }
    }
}

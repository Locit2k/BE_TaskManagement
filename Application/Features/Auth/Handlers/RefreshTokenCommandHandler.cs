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
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<RefreshTokenCommandHandler> _logger;
        public RefreshTokenCommandHandler(IAuthService authService, IUnitOfWork unitOfWork, ILogger<RefreshTokenCommandHandler> logger)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTOResponse<string>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authService.RefreshToken(request.UserName);
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
                    MessageError = "Hệ thống thực thi không thành công. Vui lòng thử lại hoặc liên hệ nhà phát triển."
                };
            }
        }
    }
}

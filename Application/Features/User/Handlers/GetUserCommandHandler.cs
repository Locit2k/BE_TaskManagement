using Application.DTOs;
using Application.Features.User.Commands;
using Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Application.Features.User.Handlers
{
    internal class GetUserCommandHandler : IRequestHandler<GetUserCommand, DTOResponse<DTOUser>>
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetUserCommandHandler> _logger;
        public GetUserCommandHandler(IUserService userService, ILogger<GetUserCommandHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<DTOResponse<DTOUser>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dtoUser = await _userService.GetUserByUserName(request.UserName);
                return new DTOResponse<DTOUser>()
                {
                    IsError = false,
                    Data = dtoUser
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<DTOUser>()
                {
                    IsError = true
                };
            }
        }
    }
}

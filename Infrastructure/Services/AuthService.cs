using Application.DTOs;
using Application.Interfaces.Authentications;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordProvider _passwordProvider;
        private readonly IUserService _userService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IJwtProvider jwtProvider, IPasswordProvider passwordProvider, IUserService userService, ILogger<AuthService> logger)
        {
            _jwtProvider = jwtProvider;
            _passwordProvider = passwordProvider;
            _userService = userService;
            _logger = logger;
        }
    }
}

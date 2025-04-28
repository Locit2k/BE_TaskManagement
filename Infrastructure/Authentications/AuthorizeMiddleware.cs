using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentications
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IRepository<Users> userRepository)
        {
            var enpoint = context.GetEndpoint();
            if (enpoint != null && enpoint.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }

            if (context.User == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var identity = context.User.Identity;
            if (identity == null || !identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var userName = context.User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var user = await userRepository.GetOneAsync(x => x.UserName == userName);
            if (user == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token không hợp lệ.");
                return;
            }
            await _next(context);
        }
    }
}

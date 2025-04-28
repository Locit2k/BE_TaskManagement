using Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<DTOResponse<string>> Login(string username, string password);

        Task<DTOResponse<string>> Register(DTORegister data);

        Task<DTOResponse<string>> RefreshToken(string userName);

        void SetRefreshTokenCookie(HttpContext context, string refreshToken);

    }
}

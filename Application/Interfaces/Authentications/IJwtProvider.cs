using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Authentications
{
    public interface IJwtProvider
    {
        string GenerateToken(DTOUser user);
        ClaimsPrincipal? ValidateToken(string token);
        string GenerateRefreshToken();

    }
}

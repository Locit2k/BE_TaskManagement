using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<DTOResponse<DTOToken>> Login(string username, string password);

        Task<DTOResponse<DTOToken>> RefreshToken(string userName, string refreshToken);

        Task<DTOResponse<DTOToken>> Register(DTORegister data);


    }
}

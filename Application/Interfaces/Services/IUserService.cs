using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<DTOUser?> GetUserLogin(string username, string password);
        Task<bool> CheckUserNameAsync(string username);
        Task<bool> CheckEmailAsync(string email);
        void Add(Users data);
        Task UpdateRefreshToken(string userName, string refreshToken);
        Task<DTOTokenUser?> GetUserByRefreshToken(string userName, string refreshToken);
    }
}

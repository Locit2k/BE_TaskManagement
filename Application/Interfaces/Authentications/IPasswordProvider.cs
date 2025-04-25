using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Authentications
{
    public interface IPasswordProvider
    {
        string HashPassword(object user, string password);
        bool VerifyPassword(object user, string hashedPassword, string inputPassword);
    }
}

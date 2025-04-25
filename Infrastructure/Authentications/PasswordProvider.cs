using Application.DTOs;
using Application.Interfaces.Authentications;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentications
{
    public class PasswordProvider : IPasswordProvider
    {
        private readonly PasswordHasher<object> _hasher = new();
        public string HashPassword(object user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(object user, string hashedPassword, string inputPassword)
        {
            var result = _hasher.VerifyHashedPassword(user, hashedPassword, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}

using Application.DTOs;
using Application.Interfaces.Authentications;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Authentications;
using Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordProvider _passwordProvider;
        private readonly IRepository<Users> _userRepossitory;
        private readonly IRepository<Employees> _employeeRepository;
        private readonly IRepository<Roles> _roleRepository;
        private JwtSetting _jwtSettings;
        private readonly ILogger<UserService> _logger;
        public UserService(IRepository<Users> userRepossitory, IRepository<Employees> employeeRepository, IRepository<Roles> roleRepository, IPasswordProvider passwordProvider, IUnitOfWork unitOfWork, IOptions<JwtSetting> jwtSettings, ILogger<UserService> logger)
        {
            _userRepossitory = userRepossitory;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _passwordProvider = passwordProvider;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        public async Task<DTOUser?> GetUserLogin(string username, string password)
        {
            var user = await _userRepossitory.GetOneAsync(x => x.UserName == username);
            if (user == null)
            {
                return null;
            };

            var validPassword = _passwordProvider.VerifyPassword(user, user.Password, password);
            if (!validPassword)
            {
                return null;
            }

            var result = new DTOUser()
            {
                UserName = user.UserName
            };
            Guid.TryParse(user.RoleID, out var gRoleID);
            var role = await _roleRepository.GetOneAsync(x => x.RecID == gRoleID);
            if (role != null)
            {
                result.Role = role.RoleName;
            }

            var employee = await _employeeRepository.GetOneAsync(x => x.EmployeeID == user.EmployeeID);
            if (employee != null)
            {
                result.FullName = employee.EmployeeName;
                result.Gender = employee.Gender;
                result.Birthday = employee.Birthday;
                result.Phone = employee.Phone;
                result.Email = employee.Email;
                result.Address = employee.Address;
            }
            return result;
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            return await _employeeRepository.ExistAsync(x => x.Email == email);

        }

        public async Task<bool> CheckUserNameAsync(string username)
        {
            return await _userRepossitory.ExistAsync(x => x.UserName == username);
        }

        public void Add(Users data)
        {
            _userRepossitory.Add(data);
        }

        public async Task UpdateRefreshToken(string userName, string refreshToken)
        {
            var user = await _userRepossitory.GetOneAsync(x => x.UserName == userName);
            if (user == null)
            {
                _logger.LogError($"Not found Users with UserName = {userName}");
                return;
            }
            user.RefreshToken = refreshToken;
            user.RefreshTokenExperies = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpiresInMinutes);
            user.ModifiedBy = userName;
            user.ModifiedOn = DateTime.UtcNow;
            _userRepossitory.Update(user);
        }

        public async Task<DTOTokenUser?> GetUserByRefreshToken(string userName, string refreshToken)
        {
            var user = await _userRepossitory.GetOneAsync(x => x.UserName == userName && x.RefreshToken == refreshToken);
            if (user == null)
            {
                _logger.LogError($"Not found Users with UserName = {userName} and RefreshToken {refreshToken}");
                return null;
            }

            var result = new DTOTokenUser()
            {
                UserName = user.UserName,
                RefreshToken = refreshToken,
                RefreshTokenExperies = user.RefreshTokenExperies
            };
            Guid.TryParse(user.RoleID, out var gRoleID);
            var role = await _roleRepository.GetOneAsync(x => x.RecID == gRoleID);
            if (role != null)
            {
                result.Role = role.RoleName;
            }

            var employee = await _employeeRepository.GetOneAsync(x => x.EmployeeID == user.EmployeeID);
            if (employee != null)
            {
                result.FullName = employee.EmployeeName;
                result.Gender = employee.Gender;
                result.Birthday = employee.Birthday;
                result.Phone = employee.Phone;
                result.Email = employee.Email;
                result.Address = employee.Address;
            }
            return result;
        }
    }
}

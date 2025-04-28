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
        private readonly IRepository<Users> _userRepository;
        private readonly IRepository<Employees> _employeeRepository;
        private readonly IRepository<Roles> _roleRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IRepository<Users> userRepository, IRepository<Employees> employeeRepository, IRepository<Roles> roleRepository, IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public void Add(Users data)
        {
            _userRepository.Add(data);
        }

        public async Task<DTOUser> GetUserByUserName(string userName)
        {
            var user = await _userRepository.GetOneAsync(x => x.UserName == userName);
            if (user == null) return null;
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
    }
}

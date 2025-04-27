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
        private JwtSetting _jwtSettings;
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
    }
}

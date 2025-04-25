using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Roles> _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RoleService> _logger;
        public RoleService(IRepository<Roles> roleRepository, IUnitOfWork unitOfWork, ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTORole?> GetByRoleName(string roleName)
        {
            DTORole? response = null;
            var role = await _roleRepository.GetOneAsync(x => x.RoleName == roleName);
            if (role != null)
            {
                response = new DTORole()
                {
                    RecID = role.RecID,
                    RoleName = roleName
                };
            }
            return response;
        }
    }
}

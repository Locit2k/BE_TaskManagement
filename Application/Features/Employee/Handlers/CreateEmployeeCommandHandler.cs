using Application.DTOs;
using Application.Features.Employee.Commands;
using Application.Interfaces.Authentications;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Features.Employee.Handlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, DTOResponse<DTOEmployee>>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;

        public CreateEmployeeCommandHandler(IEmployeeService employeeService, IUnitOfWork unitOfWork, ILogger<CreateEmployeeCommandHandler> logger)
        {
            _employeeService = employeeService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTOResponse<DTOEmployee>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = new Employees()
                {
                    RecID = Guid.NewGuid(),
                    EmployeeID = request.EmployeeID,
                    EmployeeName = request.EmployeeName,
                    Gender = request.Gender,
                    Birthday = request.Birthday,
                    Email = request.Email,
                    Phone = request.Phone,
                    Address = request.Address,
                    CreatedBy = request.CreatedBy,
                    CreatedOn = DateTime.Now
                };
                _employeeService.Add(employee);
                await _unitOfWork.SaveAsync();
                var dtoEmployee = new DTOEmployee()
                {
                    RecID = employee.RecID,
                    EmployeeID = employee.EmployeeID,
                    EmployeeName = employee.EmployeeName,
                    Gender = employee.Gender,
                    Birthday = employee.Birthday,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Address = employee.Address,
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn
                };
                return new DTOResponse<DTOEmployee>()
                {
                    IsError = false,
                    Data = dtoEmployee
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<DTOEmployee>()
                {
                    IsError = true,
                    ErrorType = "3",
                    MessageError = "Thêm không thành công."
                };
            }
        }
    }
}

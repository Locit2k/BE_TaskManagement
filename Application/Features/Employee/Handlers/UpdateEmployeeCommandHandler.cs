using Application.DTOs;
using Application.Features.Employee.Commands;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
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
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, DTOResponse<DTOEmployee>>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger;

        public UpdateEmployeeCommandHandler(IEmployeeService employeeService, IUnitOfWork unitOfWork, ILogger<UpdateEmployeeCommandHandler> logger)
        {
            _employeeService = employeeService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTOResponse<DTOEmployee>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeService.GetOneAsync(x => x.EmployeeID == request.EmployeeID);
                if (employee == null)
                {
                    return new DTOResponse<DTOEmployee>()
                    {
                        IsError = true,
                        ErrorType = "2",
                        MessageError = "Cập nhật không thành công. Không tìm thấy thông tin nhân viên."
                    };
                }

                employee.EmployeeName = request.EmployeeName;
                employee.Gender = request.Gender;
                employee.Birthday = request.Birthday;
                employee.Email = request.Email;
                employee.Phone = request.Phone;
                employee.Address = request.Address;
                employee.ModifiedBy = request.ModifiedBy;
                employee.ModifiedOn = DateTime.Now;
                _employeeService.Update(employee);
                await _unitOfWork.SaveAsync();
                return new DTOResponse<DTOEmployee>()
                {
                    IsError = false,
                    MessageError = "Cập nhật thành công."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<DTOEmployee>()
                {
                    IsError = true,
                    ErrorType = "2",
                    MessageError = "Cập nhật không thành công."
                };
            }
        }
    }
}

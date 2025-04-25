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
    public class RemoveEmployeeCommandHandler : IRequestHandler<RemoveEmployeeCommand, DTOResponse<bool>>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveEmployeeCommandHandler> _logger;

        public RemoveEmployeeCommandHandler(IEmployeeService employeeService, IUnitOfWork unitOfWork, ILogger<RemoveEmployeeCommandHandler> logger)
        {
            _employeeService = employeeService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTOResponse<bool>> Handle(RemoveEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeService.GetOneAsync(x => x.EmployeeID == request.EmployeeID);
                if (employee == null)
                {
                    return new DTOResponse<bool>()
                    {
                        IsError = true,
                        ErrorType = "2",
                        MessageError = "Xóa không thành công. Không tìm thấy thông tin nhân viên."
                    };
                }

                _employeeService.Delete(employee);
                await _unitOfWork.SaveAsync();
                return new DTOResponse<bool>()
                {
                    IsError = false,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<bool>()
                {
                    IsError = true,
                    ErrorType = "2",
                    MessageError = "Xóa không thành công."
                };
            }
        }
    }
}

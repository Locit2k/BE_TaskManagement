using Application.DTOs;
using Application.Features.Employee.Commands;
using Application.Features.Employee.Handlers;
using Application.Features.Task.Commands;
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

namespace Application.Features.Task.Handlers
{
    public class RemoveTaskCommandHandler : IRequestHandler<RemoveTaskCommand, DTOResponse<bool>>
    {
        private readonly ITaskService _taskService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveTaskCommandHandler> _logger;

        public RemoveTaskCommandHandler(ITaskService taskService, IUnitOfWork unitOfWork, ILogger<RemoveTaskCommandHandler> logger)
        {
            _taskService = taskService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTOResponse<bool>> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dtoTask = new DTOTask()
                {
                    RecID = request.RecID
                };
                await _taskService.Delete(dtoTask);
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

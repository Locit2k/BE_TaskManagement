using Application.DTOs;
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
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, DTOResponse<DTOTask>>
    {
        private readonly ITaskService _taskService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateTaskCommandHandler> _logger;

        public UpdateTaskCommandHandler(ITaskService taskService, IUnitOfWork unitOfWork, ILogger<UpdateTaskCommandHandler> logger)
        {
            _taskService = taskService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTOResponse<DTOTask>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dtoTask = new DTOTask()
                {
                    RecID = request.RecID,
                    Title = request.Title,
                    Description = request.Description,
                    Priority = request.Priority,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Status = request.Status,
                    Owner = request.Owner,
                    AssignBy = request.AssignBy,
                    ModifiedBy = request.ModifiedBy
                };
                await _taskService.Update(dtoTask);
                await _unitOfWork.SaveAsync();
                return new DTOResponse<DTOTask>()
                {
                    IsError = false,
                    Data = dtoTask
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonSerializer.Serialize(ex));
                return new DTOResponse<DTOTask>()
                {
                    IsError = true,
                    ErrorType = "2",
                    MessageError = "Cập nhật không thành công."
                };
            }
        }
    }
}

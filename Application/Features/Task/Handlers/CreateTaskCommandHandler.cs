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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.Task.Handlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, DTOResponse<DTOTask>>
    {
        private readonly ITaskService _taskService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTaskCommandHandler> _logger;

        public CreateTaskCommandHandler(ITaskService taskService, IUnitOfWork unitOfWork, ILogger<CreateTaskCommandHandler> logger)
        {
            _taskService = taskService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<DTOResponse<DTOTask>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
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
                    CreatedBy = request.CreatedBy
                };
                _taskService.Add(dtoTask);
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
                    MessageError = "Thêm không thành công."
                };
            }
        }
    }
}

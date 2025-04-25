using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Tasks> _repository;
        private readonly ILogger<TaskService> _logger;
        public TaskService(IRepository<Tasks> repository, ILogger<TaskService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void Add(DTOTask data)
        {
            if (data == null)
            {
                _logger.LogInformation("Data is null");
                throw new ArgumentNullException(nameof(data));
            }
            var task = new Tasks()
            {
                RecID = data.RecID,
                Title = data.Title,
                Description = data.Description,
                Priority = data.Priority,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Status = data.Status,
                Owner = data.Owner,
                AssignBy = data.AssignBy,
                CreatedBy = data.CreatedBy,
                CreatedOn = DateTime.Now
            };
            _repository.Add(task);
        }

        public async Task Delete(DTOTask data)
        {
            if (data == null)
            {
                _logger.LogInformation("Data is null");
                throw new ArgumentNullException(nameof(data));
            }
            var task = await _repository.GetOneAsync(x => x.RecID == data.RecID);
            if (task != null)
            {
                _repository.Delete(task);
            }
        }

        public async Task Update(DTOTask data)
        {
            if (data == null)
            {
                _logger.LogInformation("Data is null");
                throw new ArgumentNullException(nameof(data));
            }
            var task = await _repository.GetOneAsync(x => x.RecID == data.RecID);
            if (task != null)
            {
                task.Title = data.Title;
                task.Description = data.Description;
                task.Priority = data.Priority;
                task.StartDate = data.StartDate;
                task.EndDate = data.EndDate;
                task.Status = data.Status;
                task.Owner = data.Owner;
                task.AssignBy = data.AssignBy;
                task.ModifiedOn = DateTime.Now;
                _repository.Update(task);
            }
        }

        public async Task<Tasks?> GetOneAsync(Expression<Func<Tasks, bool>> predicate)
        {
            return await _repository.GetOneAsync(predicate);
        }
    }
}

using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task<Tasks?> GetOneAsync(Expression<Func<Tasks, bool>> predicate);
        void Add(DTOTask data);
        Task Update(DTOTask data);
        Task Delete(DTOTask data);
    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Employees?> GetOneAsync(Expression<Func<Employees, bool>> predicate);
        void Add(Employees data);
        void Update(Employees data);
        void Delete(Employees data);
    }
}

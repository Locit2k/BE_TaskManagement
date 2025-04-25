using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Add(T data);
        void Update(T data);
        void Delete(T data);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);


    }
}

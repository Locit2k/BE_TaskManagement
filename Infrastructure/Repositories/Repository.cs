using Application.Interfaces.Repositories;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly TaskManagementDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(TaskManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T data)
        {
            _dbSet.Add(data);
        }
        public void Update(T data)
        {
            _dbSet.Update(data);
        }

        public void Delete(T data)
        {
            _dbSet.Remove(data);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var response = await _dbSet.ToListAsync();
            return response;
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var response = await _dbSet.Where(predicate).ToListAsync();
            return response;
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate)
        {
            var response = await _dbSet.FirstOrDefaultAsync(predicate);
            return response;
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            var response = await _dbSet.AnyAsync(predicate);
            return response;
        }
    }
}

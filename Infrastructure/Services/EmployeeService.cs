using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employees> _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IRepository<Employees> employeeService, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeService;
            _logger = logger;
        }
        public void Add(Employees data)
        {
            _employeeRepository.Add(data);
        }

        public void Update(Employees data)
        {
            _employeeRepository.Update(data);
        }

        public void Delete(Employees data)
        {
            _employeeRepository.Delete(data);
        }

        public string GenerateEmployeeID()
        {
            var id = "";
            string day, month, year, hr, min, sec;
            DateTime date = DateTime.Now;
            day = date.Date.Day.ToString();
            month = date.Month.ToString();
            year = date.Year.ToString();
            hr = date.Hour.ToString();
            min = date.Minute.ToString();
            sec = date.Second.ToString();
            id = "E-" + day + month + year + hr + min + sec;
            return id;
        }

        public async Task<Employees?> GetOneAsync(Expression<Func<Employees, bool>> predicate)
        {
            return await _employeeRepository.GetOneAsync(predicate);
        }
    }
}

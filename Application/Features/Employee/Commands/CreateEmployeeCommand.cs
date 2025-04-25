using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employee.Commands
{
    public class CreateEmployeeCommand : IRequest<DTOResponse<DTOEmployee>>
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
    }
}

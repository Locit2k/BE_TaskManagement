using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employee.Commands
{
    public class RemoveEmployeeCommand : IRequest<DTOResponse<bool>>
    {
        public string EmployeeID { get; set; }
    }
}

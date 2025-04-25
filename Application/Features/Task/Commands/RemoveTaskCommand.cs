using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Task.Commands
{
    public class RemoveTaskCommand : IRequest<DTOResponse<bool>>
    {
        public Guid RecID { get; set; }
    }
}

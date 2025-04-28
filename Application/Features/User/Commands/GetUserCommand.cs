using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public class GetUserCommand : IRequest<DTOResponse<DTOUser>>
    {
        public string UserName { get; set; }
    }
}

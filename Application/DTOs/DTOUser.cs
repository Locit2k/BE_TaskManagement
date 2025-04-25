using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DTOUser : IEntity
    {
        public Guid RecID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
    }
}

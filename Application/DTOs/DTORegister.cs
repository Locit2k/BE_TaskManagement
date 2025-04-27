using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DTORegister
    {
        public Guid RecID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

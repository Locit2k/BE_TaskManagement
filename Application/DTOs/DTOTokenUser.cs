using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DTOTokenUser : DTOUser
    {
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExperies { get; set; }

    }
}

using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Roles : IEntity
    {
        public Roles()
        {
            RecID = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }
        public Guid RecID { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

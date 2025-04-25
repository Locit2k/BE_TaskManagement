using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employees : IEntity
    {
        public Employees()
        {
            RecID = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }
        public Guid RecID { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Users : IEntity
    {
        public Guid RecID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeID { get; set; }
        public string RoleID { get; set; }
        /// <summary>
        /// 1. Đang sử dụng
        /// 0. Khóa
        /// </summary>
        public string Status { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExperies { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

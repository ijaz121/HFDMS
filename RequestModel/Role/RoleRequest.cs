using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestModel.Role
{
    public class RoleRequest
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Permissions { get; set; }
        public string? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}

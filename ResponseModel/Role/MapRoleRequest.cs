using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseModel.Role
{
    public class MapRoleRequest
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? IsDeleted { get; set; }
        public string? UserName { get; set; }
        public List<RoleMapping>? roleMappings { get; set; }
    }
    
    public class RoleMapping
    {
        public int ActivityId { get; set; }
        public bool CanView { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanCreate { get; set; }
    }

    public class MappedDataRequest
    {
        public string? RoleId { get; set; }
        public string? UserId{ get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseModel
{
    public class LoginResponse
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? RoleName { get; set; }
        public string? RoleId { get; set; }
        public List<Permissions> Permission { get; set; }
    }

    public class Permissions
    {
        public string Page { get; set; }
    }
}

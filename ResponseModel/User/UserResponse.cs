using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseModel.User
{
    public class UserResponse
    {
        public string? UserID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RoleName { get; set; }
        public string? HealthFacilityName { get; set; }
    }

    public class DropdownData
    {
        public List<RoleDropdown> roleDropdown { get; set; }
        public List<HealthFacilityDropdown> healthFacilityDropdown { get; set; }
    }

    public class RoleDropdown
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
    }
    public class HealthFacilityDropdown
    {
        public string? HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }
    }

}

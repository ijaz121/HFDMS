using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RequestModel.User
{
    public class UserRequest
    {
        public string? UserID {get; set;}
        public string? Name {get; set;}
        public string? Email {get; set;}
        public string? Address { get; set;}
        public string? PhoneNumber {get; set;}
        public string? Role {get; set;}
        public string? HealthFacilityId { get; set; }
        public string? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}

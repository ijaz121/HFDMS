using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestModel.HealthFacility
{
    public class HealthFacilityRequest
    {
        public string? Id     {get; set;}
        public string? Name     {get; set;}
        public string? District {get; set;}
        public string? Region   {get; set;}
        public string? State    {get; set;}
        public string? country { get; set; }
        public string? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}

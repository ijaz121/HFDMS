using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseModel.HealthFacility
{
    public class GetHealthFacilityResponse
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public string? District { get; set; }
        public string? Region { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}

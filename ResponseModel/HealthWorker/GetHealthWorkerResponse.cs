using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseModel.HealthWorker
{
    public class GetHealthWorkerResponse
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? HealthFacilityID { get; set; }
    }
}

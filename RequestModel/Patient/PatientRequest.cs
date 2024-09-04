using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestModel.Patient
{
    public class PatientRequest
    {
        public string? ID {get; set; }
        public string? Name {get; set; }
        public string? Gender {get; set; }
        public string? Address {get; set; }
        public string? HealthFacilityId { get; set; }
        public string? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}

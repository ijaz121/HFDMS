using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseModel.Dashboard
{
    public class DashboardResponse
    {
        public List<PatientPerHealthFacility> patientPerHealthFacilities { get; set; }
        public List<HealthWorkersPerRegion> healthWorkers { get; set; }
        public List<GenderDistribution> genderDistributions { get; set; }

    }

    public class PatientPerHealthFacility
    {
        public string FacilityName { get; set; }
        public string PatientCount { get; set; }
    }

    public class HealthWorkersPerRegion
    {
        public string Region { get; set; }
        public string WorkerCount { get; set; }
    }

    public class GenderDistribution
    {
        public string Gender { get; set; }
        public string PatientCount { get; set; }
    }
}

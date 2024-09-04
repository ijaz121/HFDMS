using ResponseModel.BaseResponse;
using ResponseModel.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Dashboard
{
    public interface IDashboardRepo
    {
        Task<ResponseResult<List<PatientPerHealthFacility>>> PatientPerHealthFacility();
        Task<ResponseResult<List<HealthWorkersPerRegion>>> HealthWorkersPerRegion();
        Task<ResponseResult<List<GenderDistribution>>> GenderDistribution();
    }
}

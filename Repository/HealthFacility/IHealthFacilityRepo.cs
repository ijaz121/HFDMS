using RequestModel.HealthFacility;
using ResponseModel.BaseResponse;
using ResponseModel.HealthFacility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HealthFacility
{
    public interface IHealthFacilityRepo
    {
        Task<ResponseResult<string>> InsertUpdateDeleteHealthFacility(HealthFacilityRequest request);

        Task<ResponseResult<List<GetHealthFacilityResponse>>> GetHealthFacilityData(HealthFacilityRequest request);
    }
}

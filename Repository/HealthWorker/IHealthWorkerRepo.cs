using RequestModel.HealthWorker;
using ResponseModel.BaseResponse;
using ResponseModel.HealthWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HealthWorker
{
    public interface IHealthWorkerRepo
    {
        Task<ResponseResult<string>> InsertUpdateDeleteHealthWorker(HealthWorkerRequest request);

        Task<ResponseResult<List<GetHealthWorkerResponse>>> GetHealthWorkerData(HealthWorkerRequest request);
    }
}

using RequestModel.HealthWorker;
using RequestModel.Patient;
using ResponseModel.BaseResponse;
using ResponseModel.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Patient
{
    public interface IPatientRepo
    {
        Task<ResponseResult<string>> InsertUpdateDeletePatient(PatientRequest request);

        Task<ResponseResult<List<PatientResponse>>> GetPatientData(PatientRequest request);
    }
}

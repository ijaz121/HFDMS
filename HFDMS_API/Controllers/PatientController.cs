using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.HealthWorker;
using Repository.Patient;
using RequestModel.HealthWorker;
using RequestModel.Patient;
using ResponseModel.BaseResponse;

namespace HFDMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepo _patientRepo;

        public PatientController(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        [HttpPost("InsertUpdateDeletePatient")]
        public async Task<IActionResult> InsertUpdateDeletePatient(PatientRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string validationErrors = string.Join(",",
                    ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());

                    return Ok(new ResponseResult<string>
                    {
                        StatusCode = "03",
                        Message = validationErrors.ToString(),
                        Data = null
                    });
                }

                var response = await _patientRepo.InsertUpdateDeletePatient(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("GetPatientData")]
        public async Task<IActionResult> GetPatientData(PatientRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string validationErrors = string.Join(",",
                    ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());

                    return Ok(new ResponseResult<string>
                    {
                        StatusCode = "03",
                        Message = validationErrors.ToString(),
                        Data = null
                    });
                }

                var response = await _patientRepo.GetPatientData(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

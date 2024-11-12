using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.HealthFacility;
using Repository.HealthWorker;
using RequestModel.HealthFacility;
using RequestModel.HealthWorker;
using ResponseModel.BaseResponse;

namespace HFDMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthWorkerController : ControllerBase
    {
        private readonly IHealthWorkerRepo _healthWorkerRepo;

        public HealthWorkerController(IHealthWorkerRepo healthWorkerRepo)
        {
            _healthWorkerRepo = healthWorkerRepo;
        }

        [HttpPost("InsertUpdateDeleteHealthWorker")]
        public async Task<IActionResult> InsertUpdateDeleteHealthWorker(HealthWorkerRequest request)
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

                var response = await _healthWorkerRepo.InsertUpdateDeleteHealthWorker(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("GetHealthWorkerData")]
        public async Task<IActionResult> GetHealthWorkerData(HealthWorkerRequest request)
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

                var response = await _healthWorkerRepo.GetHealthWorkerData(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    
    }
}

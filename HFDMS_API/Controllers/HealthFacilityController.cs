using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.HealthFacility;
using RequestModel.HealthFacility;
using ResponseModel.BaseResponse;
using System.Net;

namespace HFDMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthFacilityController : ControllerBase
    {
        private readonly IHealthFacilityRepo _healthFacilityRepo;

        public HealthFacilityController(IHealthFacilityRepo healthFacilityRepo) 
        { 
            _healthFacilityRepo = healthFacilityRepo;
        }

        [HttpPost("InsertUpdateDeleteHealthFacility")]
        public async Task<IActionResult> InsertUpdateDeleteHealthFacility(HealthFacilityRequest request)
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

                var response = await _healthFacilityRepo.InsertUpdateDeleteHealthFacility(request);
                
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("GetHealthFacilityData")]
        public async Task<IActionResult> GetHealthFacilityData(HealthFacilityRequest request)
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

                var response = await _healthFacilityRepo.GetHealthFacilityData(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

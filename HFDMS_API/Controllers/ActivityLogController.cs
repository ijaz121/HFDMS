using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.ActivityLog;
using Repository.HealthWorker;
using RequestModel.HealthWorker;
using ResponseModel.BaseResponse;

namespace HFDMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        private readonly IActivityLogRepo _activityLogRepo;

        public ActivityLogController(IActivityLogRepo activityLogRepo)
        {
            _activityLogRepo = activityLogRepo;
        }

        [HttpGet("GetActivityLogData")]
        public async Task<IActionResult> GetActivityLogData(string RoleId)
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

                var response = await _activityLogRepo.GetActivityLogData(RoleId);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

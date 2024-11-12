using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Patient;
using Repository.UserManagement;
using RequestModel.Patient;
using RequestModel.User;
using ResponseModel.BaseResponse;

namespace HFDMS_API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementRepo _userManagementRepo;

        public UserController(IUserManagementRepo userManagementRepo)
        {
            _userManagementRepo = userManagementRepo;
        }

        [HttpPost("InsertUpdateDeleteUser")]
        public async Task<IActionResult> InsertUpdateDeleteUser(UserRequest request)
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

                var response = await _userManagementRepo.InsertUpdateDeleteUser(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("GetUserData")]
        public async Task<IActionResult> GetUserData(UserRequest request)
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

                var response = await _userManagementRepo.GetUserData(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetDropdownData")]
        public async Task<IActionResult> GetDropdownData()
        {
            try
            {
                var response = await _userManagementRepo.RoleHealthFacilityDropdownData();
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

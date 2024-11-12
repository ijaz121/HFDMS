using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.RoleManagement;
using Repository.UserManagement;
using RequestModel.Role;
using RequestModel.User;
using ResponseModel.BaseResponse;
using ResponseModel.Role;

namespace HFDMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleManagementRepo _roleManagementRepo;

        public RoleController(IRoleManagementRepo roleManagementRepo)
        {
            _roleManagementRepo = roleManagementRepo;
        }

        [HttpPost("InsertUpdateDeleteRole")]
        public async Task<IActionResult> InsertUpdateDeleteRole(RoleRequest request)
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

                var response = await _roleManagementRepo.InsertUpdateDeleteRole(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("GetRoleData")]
        public async Task<IActionResult> GetRoleData(UserRequest request)
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

                var response = await _roleManagementRepo.GetRoleData(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("MapRole")]
        public async Task<IActionResult> MapRole(MapRoleRequest request)
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

                var response = await _roleManagementRepo.MapRole(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost("GetMappedData")]
        public async Task<IActionResult> GetMappedData(MappedDataRequest request)
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

                var response = await _roleManagementRepo.GetMappedData(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

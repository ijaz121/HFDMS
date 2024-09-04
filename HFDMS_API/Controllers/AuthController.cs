using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Login;
using Repository.RoleManagement;
using RequestModel.Login;
using RequestModel.User;
using ResponseModel.BaseResponse;

namespace HFDMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginRepo _loginRepo;

        public AuthController(ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
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

                var response = await _loginRepo.Login(request);

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

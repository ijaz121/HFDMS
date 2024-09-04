using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Dashboard;
using Repository.Login;
using ResponseModel.BaseResponse;

namespace HFDMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepo _dashboardRepo;

        public DashboardController(IDashboardRepo dashboardRepo)
        {
            _dashboardRepo = dashboardRepo;
        }

        [HttpGet("GetPatientPerHealthFacility")]
        public async Task<IActionResult> GetPatientPerHealthFacility()
        {
            try
            {
                var response = await _dashboardRepo.PatientPerHealthFacility();

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetHealthWorkersPerRegion")]
        public async Task<IActionResult> GetHealthWorkersPerRegion()
        {
            try
            {
                var response = await _dashboardRepo.HealthWorkersPerRegion();

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GenderDistribution")]
        public async Task<IActionResult> GenderDistribution()
        {
            try
            {
                var response = await _dashboardRepo.GenderDistribution();

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

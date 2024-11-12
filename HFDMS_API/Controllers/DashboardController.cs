using CommonServices.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Dashboard;
using Repository.Login;
using ResponseModel.BaseResponse;

namespace HFDMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepo _dashboardRepo;
        private readonly IClaimValues _claimValues;

        public DashboardController(IDashboardRepo dashboardRepo, IClaimValues claimValues)
        {
            _dashboardRepo = dashboardRepo;
            _claimValues = claimValues;
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

using Microsoft.AspNetCore.Mvc;
using VMS.API.Services;
using VMS.API.Models;

namespace VMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DatabaseService _dbService;

        public DashboardController(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Dashboard>>> GetDashboardData()
        {
            try
            {
                var data = await _dbService.GetDashboardDataAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
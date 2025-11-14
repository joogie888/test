using Microsoft.AspNetCore.Mvc;
using VMS.API.Services;
using VMS.API.Models;

namespace VMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly DatabaseService _dbService;

        public ReportsController(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Report>>> GetReports()
        {
            try
            {
                var reports = await _dbService.GetReportsAsync();
                return Ok(reports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("daterange")]
        public async Task<ActionResult<List<Report>>> GetReportsByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var reports = await _dbService.GetReportsByDateRangeAsync(startDate, endDate);
                return Ok(reports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
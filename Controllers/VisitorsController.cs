using Microsoft.AspNetCore.Mvc;
using VMS.API.Services;
using VMS.API.Models;

namespace VMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitorsController : ControllerBase
    {
        private readonly DatabaseService _dbService;

        public VisitorsController(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Visitors>>> GetVisitors()
        {
            try
            {
                var visitors = await _dbService.GetVisitorsAsync();
                return Ok(visitors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Visitors>> GetVisitor(int id)
        {
            try
            {
                var visitor = await _dbService.GetVisitorByIdAsync(id);
                if (visitor == null)
                    return NotFound();

                return Ok(visitor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddVisitor([FromBody] Visitors visitor)
        {
            try
            {
                var newId = await _dbService.AddVisitorAsync(visitor);
                return Ok(newId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using VMS.API.Services;
using VMS.API.Models;

namespace VMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseService _dbService;

        // ⭐ Constructor that receives DatabaseService
        public UsersController(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetUsers()
        {
            try
            {
                var users = await _dbService.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
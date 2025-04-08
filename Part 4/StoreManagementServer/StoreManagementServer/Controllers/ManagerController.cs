using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.DTO;
using StoreManagement.Repo.IRepositories;

namespace StoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        [HttpGet("GetManager")]
        public async Task<ActionResult<ManagerDTO>> GetManager()
        {
            try
            {
                var manager = await _managerRepository.GetManagerAsync();
                if (manager == null)
                    return NotFound("Manager not found.");
                return Ok(manager);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddManager")]
        public async Task<ActionResult<ManagerDTO>> AddManager([FromBody] ManagerDTO manager)
        {
            if (manager == null)
                return BadRequest("Manager data is null.");

            try
            {
                var createdManager = await _managerRepository.AddAsync(manager);
                return CreatedAtAction(nameof(GetManager), new { }, createdManager);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateManager")]
        public async Task<IActionResult> UpdateManager([FromBody] ManagerDTO manager)
        {
            if (manager == null)
                return BadRequest("Manager data is null.");
            try
            {
                await _managerRepository.UpdateAsync(manager);
                return Ok("Manager updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

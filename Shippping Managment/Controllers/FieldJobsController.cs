using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldJobsController : ControllerBase
    {
        private readonly IRepositry<FieldJob> _service;

        public FieldJobsController(IRepositry<FieldJob> service)
        {
            _service = service;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _service.GetAllAsync();
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            var permission = await _service.GetAsyncById(id);
            if (permission == null)
            {
                return NotFound();
            }
            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> AddPermission([FromBody] FieldJob permission)
        {
            await _service.CreateAsync(permission);
            await _service.SaveAsync();
            return CreatedAtAction(nameof(GetPermissionById), new { id = permission.ID }, permission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(int id, [FromBody] FieldJob permission)
        {
            if (id != permission.ID)
            {
                return BadRequest();
            }

            _service.Update(permission);
            await _service.SaveAsync(); // Added SaveAsync to persist updates
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveAsync(); // Added SaveAsync to persist deletions
            return NoContent();
        }
    }
}

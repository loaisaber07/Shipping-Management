using Business_Layer.Services.Employee;
using Data_Access_Layer.DTO.Employee;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUser userRepo;

        public EmployeeController(IUser userRepo)
        {
            this.userRepo = userRepo;
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployee() { 
        IEnumerable<ApplicationUser> users =await userRepo.GetAllEmployee();
        IEnumerable<DisplayEmployeeDTO>dto =   EmployeeServices.GetEmployees(users);
        return Ok(dto);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(string employeeId)
        {
           bool chick = await userRepo.DeleteUserAsync(employeeId);
            if (!chick)
            {
                return BadRequest(new { Message = "Can not delete try agin" });
            }
           await userRepo.SaveAsync();
            return Ok(new {Message="Employee Deleted"});
        }
    }
}

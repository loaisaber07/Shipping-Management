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
        [HttpPut]
        public async Task<ActionResult>  EditEmployee(EditEmployeeDTO dto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(new { Message = "invalid data" });      
            }
        ApplicationUser? user =  await userRepo.GetUserAsyncById(dto.ID);
            if (user is null) {
                return NotFound(new { Message="Can't find Employee hava the same id!"});
            }
      user= EmployeeServices.MapEmployeeForEditing(user, dto); ;
     bool check= await userRepo.UpdateUser(user);
            if (!check) { 
            return BadRequest(new { Message="Can't update Employee!"});
            }
            await userRepo.SaveAsync();
            return Ok(); 

        }
    }
}

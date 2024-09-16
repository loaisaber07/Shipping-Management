using Business_Layer.DTO.Employee;
using Business_Layer.Services.Employee;
using Business_Layer.Services.Seller;
using Data_Access_Layer.DTO.Seller;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IBranch branchRepo;
        private readonly IFieldJob fieldRepo;
        private readonly IUser userReo;

        public RegisterController(IBranch branchRepo ,IFieldJob fieldRepo ,IUser userReo)
        {
            this.branchRepo = branchRepo;
            this.fieldRepo = fieldRepo;
            this.userReo = userReo;
        }
        [HttpPost]
        [Route("/AddEmployee")]
        public async Task <ActionResult> AddEmployee(AddEmployeeDTO employee )
        {
            if (!ModelState.IsValid) {
                return BadRequest(new { Message = "Incorrect Data!" }); 
            }
            bool check =await branchRepo.IsExistByID(employee.BranchID);
            if (!check) { 
            return BadRequest(new { Message="Branch Not Exist"});
            }
            check = await fieldRepo.IsExistByIdAsync(employee.FieldJobID);
            if (!check) {
                return BadRequest(new { Message = "FieldJob is Not Found" }); 
            }
            ApplicationUser user = EmployeeServices.GetEmployee(employee);
             bool result= await userReo.CreateUser(user, employee.Password);
            if (!result) {
                return BadRequest(new { Message = "Failed to create new employee!" }); 
            }
             result= await  userReo.AddRole(employee.Email, "Employee");
            if (!result) {
                return BadRequest(); 
            } 

            return Ok(new { Message="Adding Successfully"}); 
        }
        [HttpPost]
        [Route("/AddSeller")]
        public async Task<ActionResult> AddSeller(AddSellerDTO sellerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Incorrect Data!" });
            }
            bool check = await branchRepo.IsExistByID(sellerDTO.BranchID);
            if (!check)
            {
                return BadRequest(new { Message = "Branch Not Exist" });
            }
            ApplicationUser user = SellerService.GetSeller(sellerDTO);
            bool result = await userReo.CreateUser(user, sellerDTO.Password);
            if (!result)
            {
                return BadRequest(new { Message = "Failed to create new seller!" });
            }
            result = await userReo.AddRole(sellerDTO.Email, "Seller");
            if (!result)
            {
                return BadRequest();
            }

            return Ok(new { Message = "Adding Successfully" });
        }
    }
}

using Business_Layer.DTO.Employee;
using Business_Layer.Services.Employee;
using Business_Layer.Services.Seller;
using Business_Layer.Services.SpecialCharge;
using Data_Access_Layer.DTO.Seller;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISpecialCharge specialChargeRepo;

        public RegisterController(IBranch branchRepo ,IFieldJob fieldRepo ,IUser userReo, 
            RoleManager<IdentityRole> roleManager , 
            ISpecialCharge specialChargeRepo)
        {
            this.branchRepo = branchRepo;
            this.fieldRepo = fieldRepo;
            this.userReo = userReo;
            this._roleManager = roleManager;
            this.specialChargeRepo = specialChargeRepo;
        }
        [HttpPost]
        [Route("/AddEmployee")]
        public async Task <ActionResult> AddEmployee(AddEmployeeDTO employee )
        {
            if (!ModelState.IsValid) {
                return BadRequest(new { Message = "Incorrect Data!" }); 
            }
            var roleExists = await _roleManager.RoleExistsAsync("Employee");
            if (!roleExists)
            {
                var role = new IdentityRole("Employee");
                var result1 = await _roleManager.CreateAsync(role);

                if (!result1.Succeeded)
                {
                    return BadRequest(new { Message="Can't create the Role of Employee successfully!"});
                }
               
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
            var roleExists = await _roleManager.RoleExistsAsync("Seller");
            if (!roleExists)
            {
                var role = new IdentityRole("Seller");
                var result1 = await _roleManager.CreateAsync(role);

                if (!result1.Succeeded)
                {
                    return BadRequest(new { Message = "Can't create the Role of Seller successfully!" });
                }

            }
            bool check = await branchRepo.IsExistByID(sellerDTO.BranchID);
            if (!check)
            {
                return BadRequest(new { Message = "Branch Not Exist" });
            }
            Seller user = SellerService.GetSeller(sellerDTO);
            bool result = await userReo.CreateSeller(user, sellerDTO.Password);
            if (!result)
            {
                return BadRequest(new { Message = "Failed to create new seller!" });
            }
            
       IEnumerable<SpecialCharge>? list=   SpecialChargeService.GetSpecialCharges(user.Id,sellerDTO.citySellers);

            if (list is not null)
            { 
            await specialChargeRepo.BulkInsert(list);
          await specialChargeRepo.SaveAsync();
            
            }
            return Ok(new { Message = "Adding Successfully" });
        }
    }
}

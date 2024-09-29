using Business_Layer.Services.Order;
using Business_Layer.Services.Seller;
using Data_Access_Layer.DTO.Seller;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 

    public class SellerController : ControllerBase
    {
        private readonly IUser userRepo;
        private readonly ISeller sellerRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBranch branchRepo;
        private readonly IOrder orderRepo;

        public SellerController( IUser userRepo , ISeller sellerRepo , 
            UserManager<ApplicationUser> userManager , 
            IBranch branchRepo ,
            IOrder orderRepo)
        {
            this.userRepo = userRepo;
            this.sellerRepo = sellerRepo;
            this.userManager = userManager;
            this.branchRepo = branchRepo;
            this.orderRepo = orderRepo;
        }
        [HttpDelete("{sellerId:alpha}")]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult> Delete(string sellerId)
        {
           bool chick = await userRepo.DeleteSellerAsync(sellerId);
            if (!chick)
            {
                return BadRequest(new { Message = "Can not Delete Seller" });
            }
            await userRepo.SaveAsync();
            return Ok(new {Message="Seller Deleted"});

        }
        [HttpGet("{sellerId}")]
        public async Task<ActionResult> GetSellerById(string sellerId)
        {
           Seller? seller = await userRepo.GetSellerAsyncById(sellerId);
            if (seller is  null)
            {
                return NotFound(new {Message="Can not Find Seller Withe Same Id"});
            }
            GetSellerDTO getSellerDTO = SellerService.GetSellerDTO(seller); 
            return Ok(getSellerDTO);
 
        }
        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAllSellers()
        {
           IEnumerable<Seller> sellerList=  userRepo.GetAllSellers();
            IEnumerable<GetSellerDTO> get = SellerService.GetAllSellers(sellerList);
            return Ok(get);
        }
        
        [HttpGet]
        [Route("/Dashboard")]
        public async Task<ActionResult> Dashboard() { 
            string? id = HttpContext.User.FindFirst("userID")?.Value;
            ApplicationUser? user = await userManager.FindByIdAsync(id);
            if (user is null)
            {
                return Unauthorized(); 
            }
            #region for seller
            bool check = await userManager.IsInRoleAsync(user, "Seller");
            if (check)
            {
                Seller? seller = await sellerRepo.DisplayScreenForSeller(id);
                if (seller is null)
                {
                    return BadRequest();
                }
                IEnumerable<DisplayScreenForSeller?> dto = SellerService.GetDisplayScreenForSellers(seller);
                if (dto is null)
                {
                    return BadRequest();
                }
                return Ok(dto);
            }
            #endregion
            #region for employee
            check = await userManager.IsInRoleAsync(user, "Employee");
            if (check)
            {
          Branch? branch=await  branchRepo.GetOrdersInBranch(user.BranchID); 
       IEnumerable<DisplayScreenForSeller>dto= OrderService.GetDasboardForEmployee(branch.Orders); 
            return Ok(dto);
            }
            #endregion
            #region for Agent
            check = await userManager.IsInRoleAsync(user, "Agent");
            if (check)
            {
            IEnumerable<Order?>orders= await  orderRepo.GetOrderForSpecificAgent(user.Id);
            IEnumerable<DisplayScreenForSeller> dto = OrderService.GetDasboardForEmployee(orders);
            return Ok(dto);
            }
            #endregion
            #region for Admin
            check = await userManager.IsInRoleAsync(user, "Admin");
            if (check)
            { 
            IEnumerable<Order?>orders= await  orderRepo.GetOrderForAdmin();
            IEnumerable<DisplayScreenForSeller> dto = OrderService.GetDasboardForEmployee(orders);
                return Ok(dto);
            }
            #endregion
            return StatusCode(401, new { Message = "Not Authorized" });
        }



    }
}

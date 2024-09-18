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
    public class SellerController : ControllerBase
    {
        private readonly IUser userRepo;
        private readonly ISeller sellerRepo;

        public SellerController( IUser userRepo , ISeller sellerRepo)
        {
            this.userRepo = userRepo;
            this.sellerRepo = sellerRepo;
        }
        [HttpDelete]
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
        [HttpGet]
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
        public async Task<ActionResult>GetAllSellers()
        {
           IEnumerable<Seller> sellerList= await userRepo.GetAllSellers();
            IEnumerable<GetSellerDTO> get = SellerService.GetAllSellers(sellerList);
            return Ok(get);
        }
        [HttpGet]
        [Route("ScreenForSeller")]
        public async Task<ActionResult> ScreenForSeller(string id) {
          Seller? seller=await sellerRepo.DisplayScreenForSeller(id);
            if (seller is null) {
                return BadRequest();
            }
    IEnumerable<DisplayScreenForSeller?>dto  = SellerService.GetDisplayScreenForSellers(seller);
            if (dto is null) {
                return BadRequest();
            } 
            return Ok(dto);
        
        }
    }
}

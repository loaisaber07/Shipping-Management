using Data_Access_Layer.DTO;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatus statusRepo;

        public OrderStatusController(IOrderStatus statusRepo)
        {
            this.statusRepo = statusRepo;
        }
        [HttpPost]
        public async Task<ActionResult> AddOrderStatus(AddOrderStatusDTO statusDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data" });
            }
            OrderStatus status = new OrderStatus
            {
                Name = statusDTO.Name,

            };
            await statusRepo.CreateAsync(status);
            await statusRepo.SaveAsync();
            OrderStatus? orderStatus = await statusRepo.GetByName(status.Name);
            GetOrderStatusDTO get = new GetOrderStatusDTO
            {
                Id = orderStatus.ID,
                Name = statusDTO.Name,
            };
            return Ok(get);

        }
    }
}

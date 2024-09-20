using Business_Layer.Services.OrderStatus;
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
            GetOrderStatusDTO get = OrderStatusServices.OrderStatusDTO(orderStatus);
            return Ok(get);

        }
        [HttpGet]
        public async Task<ActionResult> GetAllStatus()
        {
            
            IEnumerable<OrderStatus> status = await statusRepo.GetAllAsync();
            IEnumerable<GetOrderStatusDTO> get = OrderStatusServices.MappingOrderStatus(status);

            return Ok(get);
        }
        [HttpPut]
        public async Task<ActionResult> EditOrderStatus(EditOrderStatusDTO orderStatusDTO)
        {
           
           OrderStatus? status = await statusRepo.GetAsyncById(orderStatusDTO.ID);
            if (status is null)
            {
                return NotFound(new {Message="Order Status Not Found"});
            }
            status.Name = orderStatusDTO.Name;
            if(!statusRepo.Update(status))
            {
                return BadRequest(new {Message="Can not update try again !"});    
            }
            await statusRepo.SaveAsync();
            GetOrderStatusDTO get = OrderStatusServices.OrderStatusDTO(status);
            return Ok(get);

        }
        [HttpDelete("{OrderStatusId:int}")]
        public async Task<ActionResult> DeleteOrderSatus(int OrderStatusId)
        {
          OrderStatus? orderStatus =  await statusRepo.GetAsyncById(OrderStatusId);
            if(orderStatus is null)
            {
                return NotFound(new { Message="Can not found !!" });
            }
            await statusRepo.DeleteAsync(OrderStatusId);
            await statusRepo.SaveAsync();
            return Ok(new {Message="OrderStatus Deleted"});
        }

    }
}

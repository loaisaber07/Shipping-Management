using Business_Layer.Services.Order;
using Business_Layer.Services.Products;
using Data_Access_Layer.DTO.Order;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.PixelFormats;
using System.Security.Claims;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Seller")]
    public class OrderController : ControllerBase
    {
        private readonly IOrder orderRepo;
        private readonly IProduct productRepo;
        private readonly IWeight weightRepo;

        public OrderController(IOrder orderRepo, IProduct productRepo,IWeight weightRepo)
        {
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
            this.weightRepo = weightRepo;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            IQueryable<Order> orders = orderRepo.GetAll();
            IEnumerable<GetOrderDTO> dto = OrderService.GetAllOrder(orders);
            return Ok(dto);
        }
        [HttpGet]
        [Route("GetOrderById{id:int}")]
        public async Task<ActionResult> GetOrderById(int id)
        {
           Order? order = await orderRepo.GetById(id);
            if (order == null)
            {
                return NotFound();  
            }
            GetOrderDTO orderDTO = OrderService.GetOrder(order);
            return Ok(orderDTO);
        }

        [HttpPost]
       
        public async Task<ActionResult> AddOrder(AddOrderDTO orderDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new {Message="Invalid Data !!"});
            }
        string? sellerId=  HttpContext.User.FindFirst("userID")?.Value;
            if (sellerId == null) { 
            return Unauthorized();
            }
            Order order = OrderService.MappingOrder(sellerId,orderDTO);
            await orderRepo.CreateAsync(order);
            await orderRepo.SaveAsync();
IEnumerable<Product> products =  ProductService.MappingProduct(order.ID ,orderDTO.ProductList);
            await productRepo.BulkInsert(products);
            await productRepo.SaveAsync();
            return RedirectToAction("GetAll");

        }
        [HttpDelete("{orderId:int}")]
        public async Task<ActionResult> Delete(int orderId) {

        bool check= orderRepo.ISEXIST(orderId);
            if (!check) {
                return BadRequest(new { Message = "There No Order Hava this id" }); 
            } 
check = await productRepo.BulkDelete(orderId);
            if (!check) { 
            return StatusCode(500, new { Message = "Can't Delete Try Again" });
            }
        await productRepo.SaveAsync();
await orderRepo.DeleteAsync(orderId);
          await  orderRepo.SaveAsync();
            return Ok(new { Message="Deleted Successfully "});
        }
        [HttpPut]
        public async Task<ActionResult> UpdateOrder(UpdateOrderDTO orderDTO) {
        Order? order = await orderRepo.GetById(orderDTO.ID);
            if (order is null) {
                return BadRequest(new { Message = "Not found !" }); 
            } 
            order = OrderService.MappingOrderForUpdate(order,orderDTO);
     bool check=      await productRepo.BulkUpdate(order.Products);
            if (!check) { 
            return StatusCode(500, new { Message = "Can't Update Try Again" });
            }
            await productRepo.SaveAsync(); 
            orderRepo.Update(order);
            await orderRepo.SaveAsync();
            return Ok(new { Message = "Update Successfully" });
        }

        [HttpGet("id/{id:int}")]
        public async Task<ActionResult> GetShippingCost(int id)
        {
            Order? order = await orderRepo.GetOrderForShippinCost(id);
            if (order is null)
            {
                return NotFound(new { Message = "Not found Order !" });
            }
            Weight?weight =  await orderRepo.GetWeight();
            if (weight is null)
            {
                return NotFound(new { Message = "Not found Weight Setting !" });
            }
            int shippingCost = 0;
            int typeOfChargeCost= 0;
            int vilagetCost = 0;
            int cityCost = order.City.NormalCharge;
            int weightcost = 0;
            SpecialCharge? special = await orderRepo.GetSpecialForSeller(order.CityID,order.SellerID);
            if (special is not null)
            {
                 cityCost = special.SpecialChargeForSeller;
            }
            if (order.IsForVillage)
            {
                vilagetCost = 20;
            }
            typeOfChargeCost = order.TypeOfCharge.Cost;
            if (order.Weight > weight.DefaultWeight)
            {
                 weightcost = (order.Weight - weight.DefaultWeight) * weight.AdditionalWeight;
            }
            shippingCost = cityCost + weightcost + typeOfChargeCost + vilagetCost;
            return Ok(shippingCost);

        }

    }
}

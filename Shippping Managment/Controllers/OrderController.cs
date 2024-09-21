using Business_Layer.Services.Order;
using Business_Layer.Services.Products;
using Data_Access_Layer.DTO.Order;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public OrderController(IOrder orderRepo ,IProduct productRepo)
        {
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
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
        public async Task<ActionResult> GetOrderById(int orderId)
        {
           Order? order = await orderRepo.GetById(orderId);
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

    }
}

using Business_Layer.Services.Order;
using Data_Access_Layer.DTO.Order;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            IEnumerable<Order> list = await orderRepo.GetAllAsync();
            IEnumerable<GetOrderDTO> get = OrderService.GetAllOrder(list);
            return Ok(get);
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
            Order order = OrderService.MappingOrder(orderDTO);
            await orderRepo.CreateAsync(order);
            await orderRepo.SaveAsync();
            List<Product> products = new List<Product>();
            foreach(AddProductDTO product in orderDTO.ProductList)
            {
                Product addProduct = new Product
                {
                    Name = product.Name,
                    Quantity = product.Quantity,
                    Weight = product.ProductWeight,
                    OrderID=order.ID,
                };
                products.Add(addProduct);

            }
            await productRepo.BulkInsert(products);
            await productRepo.SaveAsync();
            return Ok();

        }

    }
}

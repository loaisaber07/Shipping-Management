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
    public class OrderController : ControllerBase
    {
        private readonly IOrder orderRepo;
        private readonly IProduct productRepo;
        private readonly IWeight weightRepo;
        private readonly ISpecialCharge specialRepo;

        public OrderController(IOrder orderRepo, IProduct productRepo,IWeight weightRepo , 
            ISpecialCharge specialRepo)
        {
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
            this.weightRepo = weightRepo;
            this.specialRepo = specialRepo;
        }
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult> GetAll()
        {
            IQueryable<Order> orders = orderRepo.GetAll();
            IEnumerable<GetOrderDTO> dto = OrderService.GetAllOrder(orders);
            return Ok(dto);
        }
        [HttpGet]
        [Route("GetOrderById{id:int}")]
        [Authorize(Policy = "AdminOrSeller")]

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
        [Authorize(Policy = "Seller")]
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
        [Authorize(Policy = "AdminOrSeller")]

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
        [Authorize(Policy = "Seller")]

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

        [HttpGet]
        [AllowAnonymous]
        [Route("GetShippingCost{beignningDate:datetime}/{endingDate:datetime}/{orderStatusId:int}")]
        public async Task<ActionResult> ReportOrders(DateTime beignningDate, DateTime endingDate, int orderStatusId)
        { 
        IEnumerable<Order?>orders =await orderRepo.GetOrderByTimeAdding(beignningDate,endingDate,orderStatusId);
            if (orders is null) {
                return NotFound(new { Message = "There is no orders founded in this Date!" });
            
            }
            if (!orders.Any()) {
                return NotFound(new { Message="No Order Founded"});
            } 
            
            List<ReportOrderDTO> dtos = new List<ReportOrderDTO>();
            foreach (var order in orders)
            {
                dtos.Add(new ReportOrderDTO
                {
                    OrderID = order.ID,
                    OrderStatusName = order.OrderStatus.Name,
                    SellerName = order.Seller.UserName,
                    ClientName = order.ClientName,
                    PhoneNumber = order.ClientNumber,
                    ClientGover = order.Govern.Name,
                    ClientCity = order.City.Name,
                    OrderCost = order.Cost,
                    ChargeCost = await GetShippingCost(order),
                    OrderDate = order.DateAdding,
                    CompanyAmount = order?.Agent?.ThePrecentageOfCompanyFromOffer ?? 0
                }); 

            }
            return Ok(dtos);
        }
        private async Task<decimal> GetShippingCost(Order orders)
        {
            Order? order = orders;
            if (order is null)
            {
                return 0;
            }
            decimal cost=0m; 
            bool IsExist; 
SpecialCharge? special =  specialRepo.GetSpecialCharge(order.SellerID,order.CityID ,out IsExist);
            if (IsExist)
            {
                cost += (decimal)special.SpecialChargeForSeller;
            }
            else 
            {
                cost += order.City.NormalCharge; 
            }
            if (order.IsForVillage) {
                cost += 20; //Now i set the constatnt value for delivery to village 
            } 
            cost+= order.TypeOfCharge.Cost;
            if (order.TypeOfReceipt.Name == "Store")
            {
                cost += order.City.PickUpCharge; 
            }
            Weight weight = weightRepo.GetDefaultWeight(out IsExist);
            if (IsExist)
            {
                if (order.Weight > weight.DefaultWeight)
                { 
    cost += (order.Weight - weight.DefaultWeight) * weight.AdditionalWeight;
                }

            }
return cost;                                   
        }
         


    }
}

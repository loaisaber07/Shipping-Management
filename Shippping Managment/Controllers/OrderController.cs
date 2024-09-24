using Business_Layer.Services.Order;
using Business_Layer.Services.Products;
using Data_Access_Layer.DTO.Order;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IOrderStatus orderStatusRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public OrderController(IOrder orderRepo, IProduct productRepo,IWeight weightRepo , 
            ISpecialCharge specialRepo,IOrderStatus orderStatusRepo ,
            UserManager<ApplicationUser> userManager)
        {
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
            this.weightRepo = weightRepo;
            this.specialRepo = specialRepo;
            this.orderStatusRepo = orderStatusRepo;
            this.userManager = userManager;
        }
        [Authorize(Policy = "AdminOrEmployee")]
        [HttpGet]
        [Route("GetAll")]

        public async Task<ActionResult> GetAll()
        {
        string? userId=HttpContext.User.FindFirst("userID")?.Value;
       ApplicationUser? user =   await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Unauthorized(new { Message="Can't find the User!"});
            }
            bool check = await userManager.IsInRoleAsync(user, "Admin");
            if (check) {
                IQueryable<Order> orders = orderRepo.GetAll();
                IEnumerable<GetOrderDTO> dto = OrderService.GetAllOrder(orders);
                return Ok(dto);
            } 
            check = await userManager.IsInRoleAsync(user, "Employee");
            if (check)
            {
                IQueryable<Order> orders = orderRepo.GetOrderByBranch(user.BranchID);
                IEnumerable<GetOrderDTO> dto = OrderService.GetAllOrder(orders);
            }
            return Unauthorized(); 
            //    IQueryable<Order> orders = orderRepo.GetAll();
            //IEnumerable<GetOrderDTO> dto = OrderService.GetAllOrder(orders);
            //return Ok(dto);
        } 

        [Authorize(Policy = "AdminOrSeller")]
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
        [Authorize(Policy = "Admin")]
        [HttpPost("AdminAddOrder")]
        public async Task<ActionResult> AdminAddOrder(AdminAddOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data !!" });
            }
           OrderStatus? status = await orderStatusRepo.GetByName("New");
            if (status is null)
            {
                return BadRequest(new {Message = "Invalid Order Status Id !!" });
            }
            Order order = OrderService.AdminMappingOrder(orderDTO,status.ID);
            await orderRepo.CreateAsync(order);
            await orderRepo.SaveAsync();
            IEnumerable<Product> products = ProductService.MappingProduct(order.ID, orderDTO.ProductList);
            await productRepo.BulkInsert(products);
            await productRepo.SaveAsync();
            return Ok(new { Message = "Added Successfully " });

        }

        [Authorize( Policy = "Seller")]
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
            return Ok(new { Message="Added Successfully "});

        }
        [Authorize(Policy = "AdminOrSeller")]
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
        [Authorize(Policy = "Seller")]
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
                ReportOrderDTO report = new ReportOrderDTO();

                report.OrderID = order.ID;
                report.OrderStatusName = order.OrderStatus.Name;
                report.SellerName = order.Seller.UserName;
                report.ClientName = order.ClientName;
                report.PhoneNumber = order.ClientNumber;
                report.ClientGover = order.Govern.Name;
                report.ClientCity = order.City.Name;
                report.OrderCost = order.Cost;
                report.ChargeCost = await GetShippingCost(order);
                report.OrderDate = order.DateAdding;
                if (order?.Agent?.TypeOfOffer.Name== "Precentage" && order.Agent is not null)
                { 
                    report.CompanyAmount = (order?.Agent?.ThePrecentageOfCompanyFromOffer *report.ChargeCost) / 100;
                }
                else
                {

                report.CompanyAmount = order?.Agent?.ThePrecentageOfCompanyFromOffer ?? 0;
                }
                dtos.Add(report);

            }
            return Ok(dtos);
        }

        [Authorize(Policy = ("AdminOrEmployee"))]
        [HttpPut("EmployeeChangeOrderStatus")]
        public async Task<ActionResult> EmployeeChangeOrderStatus(EmployeeUpdateOrderStatusDTO orderDTO)
        {
            Order? order = await orderRepo.GetById(orderDTO.ID);
            if (order is null)
            {
                return BadRequest(new { Message = "Order Not found !" });
            }
           OrderStatus? modifiedStatus = await orderStatusRepo.GetAsyncById(orderDTO.OrderStatusID);
            if (modifiedStatus is null)
            {
                return NotFound(new {Message="Order Status Not Found !!"});
            }
            if (modifiedStatus.Name != "Waiting")
            {
                return BadRequest(new { Message = "Can 't Update Status" });
            }
            order.OrderStatusID = orderDTO.OrderStatusID;
                orderRepo.Update(order);
                await orderRepo.SaveAsync();    
            return Ok(new { Message = "Update Successfully" });
        }
        [Authorize(Roles =("Admin,Agent"))]
        [HttpPut("AgentChangeOrderStatus")]
        public async Task<ActionResult> AgentChangeOrderStatus(EmployeeUpdateOrderStatusDTO orderDTO)
        {
            Order? order = await orderRepo.GetById(orderDTO.ID);
            if (order is null)
            {
                return BadRequest(new { Message = "Not found !" });
            }
            OrderStatus? modifiedStatus = await orderStatusRepo.GetAsyncById(orderDTO.OrderStatusID);
            if (modifiedStatus is null)
            {
                return NotFound(new { Message = "Order Status Not Found !!" });
            }
            if (modifiedStatus.Name == "New" 
                || modifiedStatus.Name == "UnReachable"
                || modifiedStatus.Name == "AssignedToAgent")
            {
                
             return BadRequest(new { Message = "Can 't Update Status" });
            }
            order.OrderStatusID=orderDTO.OrderStatusID;
            orderRepo.Update(order);
            await orderRepo.SaveAsync();
            return Ok(new { Message = "Update Successfully" });
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

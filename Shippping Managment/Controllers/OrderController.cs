﻿using Business_Layer.Services.Order;
using Business_Layer.Services.Products;
using Data_Access_Layer.DTO.Order;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositry;
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

        public OrderController(IOrder orderRepo, IProduct productRepo, IWeight weightRepo,
            ISpecialCharge specialRepo, IOrderStatus orderStatusRepo,
            UserManager<ApplicationUser> userManager)
        {
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
            this.weightRepo = weightRepo;
            this.specialRepo = specialRepo;
            this.orderStatusRepo = orderStatusRepo;
            this.userManager = userManager;
        }
        [Authorize]
        [HttpGet]
        [Route("GetAll")]

        public async Task<ActionResult> GetAll()
        {
            string? userId = HttpContext.User.FindFirst("userID")?.Value;
            ApplicationUser? user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Unauthorized(new { Message = "Can't find the User!" });
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
                return Ok(dto);
            }
            check = await userManager.IsInRoleAsync(user, "Seller");
            if (check)
            {
                IQueryable<Order> orders = orderRepo.GetOrdersBySellerId(user.Id);
                IEnumerable<GetOrderDTO> dto = OrderService.GetAllOrder(orders);
                return Ok(dto);
            }
            check = await userManager.IsInRoleAsync(user, "Agent");
            if (check)
            {

                IQueryable<Order> orders = orderRepo.GetOrdersByAgentId(user.Id);
                IEnumerable<GetOrderDTO> dto = OrderService.GetAllOrder(orders);
                return Ok(dto);
            }
            return Unauthorized();

        }

        [Authorize(Roles = "Admin,Employee,Seller")]
        [HttpGet]
        [Route("GetOrderById/{id:int}")]
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
        [HttpPost]
        [Route("AdimnAddOrder")]
        public async Task<ActionResult> AdminAddOrder(AdminAddOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data !!" });
            }

            Order? order = OrderService.AdminMappingOrder(orderDTO, 1);
            await orderRepo.CreateAsync(order);
            await orderRepo.SaveAsync();
            IEnumerable<Product> products = ProductService.MappingProduct(order.ID, orderDTO.ProductList);
            await productRepo.BulkInsert(products);
            await productRepo.SaveAsync();
            order = await orderRepo.GetOrderForGetChargeCost(order.ID);
            order.chargeCost = await GetShippingCost(order);
            orderRepo.Update(order);
            await orderRepo.SaveAsync();
            return Ok(new { Message = "Added Successfully " });

        }

        [Authorize(Policy = "Seller")]
        [HttpPost]
        public async Task<ActionResult> AddOrder(AddOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data !!" });
            }
            string? sellerId = HttpContext.User.FindFirst("userID")?.Value;
            if (sellerId == null) {
                return Unauthorized();
            }
            Order? order = OrderService.MappingOrder(sellerId, orderDTO);
            await orderRepo.CreateAsync(order);
            await orderRepo.SaveAsync();
            IEnumerable<Product> products = ProductService.MappingProduct(order.ID, orderDTO.ProductList);
            await productRepo.BulkInsert(products);
            await productRepo.SaveAsync();
            order = await orderRepo.GetOrderForGetChargeCost(order.ID);
            order.chargeCost = await GetShippingCost(order);
            orderRepo.Update(order);
            await orderRepo.SaveAsync();
            return Ok(new { Message = "Added Successfully " });

        }
        [Authorize(Policy = "AdminOrSeller")]
        [HttpDelete("{orderId:int}")]

        public async Task<ActionResult> Delete(int orderId) {

            bool check = orderRepo.ISEXIST(orderId);
            if (!check) {
                return BadRequest(new { Message = "There No Order Hava this id" });
            }
            check = await productRepo.BulkDelete(orderId);
            if (!check) {
                return StatusCode(500, new { Message = "Can't Delete Try Again" });
            }
            await productRepo.SaveAsync();
            await orderRepo.DeleteAsync(orderId);
            await orderRepo.SaveAsync();
            return Ok(new { Message = "Deleted Successfully " });
        }
        [Authorize(Policy = "AdminOrSeller")]
        [HttpPut]
        public async Task<ActionResult> UpdateOrder(UpdateOrderDTO orderDTO) {
            Order? order = await orderRepo.GetById(orderDTO.ID);
            if (order is null) {
                return BadRequest(new { Message = "Not found !" });
            }
            order = OrderService.MappingOrderForUpdate(order, orderDTO);
            bool check = await productRepo.BulkUpdate(order.Products);
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
        [Route("Report/{beignningDate:datetime}/{endingDate:datetime}/{orderStatusId:int}")]
        public async Task<ActionResult> ReportOrders(DateTime beignningDate, DateTime endingDate, int orderStatusId)
        {
            IEnumerable<Order?> orders = await orderRepo.GetOrderByTimeAdding(beignningDate, endingDate, orderStatusId);
            if (orders is null) {
                return NotFound(new { Message = "There is no orders founded in this Date!" });

            }
            if (!orders.Any()) {
                return NotFound(new { Message = "No Order Founded" });
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
                report.ChargeCost = order.chargeCost;
                report.OrderDate = order.DateAdding;
                if (order?.Agent?.TypeOfOffer.Name == "Precentage" && order.Agent is not null)
                {
                    report.CompanyAmount = (order?.Agent?.ThePrecentageOfCompanyFromOffer * report.ChargeCost) / 100;
                }
                else
                {

                    report.CompanyAmount = order?.Agent?.ThePrecentageOfCompanyFromOffer ?? 0;
                }
                if(order.OrderStatusID==11|| order.OrderStatusID == 8)
                {
                    report.PaidCharge=order.chargeCost;
                }
                dtos.Add(report);

            }
            return Ok(dtos);
        }

        [Authorize(Roles ="Admin,Employee,Agent")]
        [HttpPut("ChangeOrderStatus")]
        public async Task<ActionResult> ChangeOrderStatus(EmployeeUpdateOrderStatusDTO orderDTO)
        {
            Order? order = await orderRepo.GetById(orderDTO.ID);
            if (order is null)
            {
                return BadRequest(new { Message = "Order Not found !" });
            }
            order.OrderStatusID = orderDTO.OrderStatusID;
            orderRepo.Update(order);
            await orderRepo.SaveAsync();
            return Ok(new { Message = "Update Successfully" });
        }

        
        [HttpPut("AssignToAgent")]
        [Authorize(Policy ="AdminOrEmployee")]
        public async Task<ActionResult> AssignToAgent(AssignToAgentDTO asignTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data" });
            }
           Order? order = await orderRepo.GetAsyncById(asignTo.OrderID);
            if (order is null)
            {
                return NotFound(new { Message = "Order Not Found !!" });
            }
            ApplicationUser? agent = await userManager.FindByIdAsync(asignTo.agentID);
            if (agent is null)
            {
                return NotFound(new { Message = "Agent Not Found !!" });
            }
            order.AgentID=asignTo.agentID;
            order.OrderStatusID = 3;
            orderRepo.Update(order);
            await orderRepo.SaveAsync();
            return Ok(new { Message = "Order Assigned To Agent" });
        }




        private async Task<decimal> GetShippingCost(Order orders)
        {
            Order? order = orders;
            if (order is null)
            {
                return 0;
            }
            decimal cost = 0m;
            bool IsExist;
            SpecialCharge? special = specialRepo.GetSpecialCharge(order.SellerID, order.CityID, out IsExist);
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
            cost += order.TypeOfCharge.Cost;
            if (order.TypeOfReceipt.Name == "Store")
            {
                if (order.Seller.PickUp > 0)
                {
                    cost += order.Seller.PickUp;
                }
                else
                {
                    cost += order.Seller.StoreCity.PickUpCharge;
                }
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

        [HttpPut("RejectOrder")]
        public async Task<ActionResult> RejectOrder(RejectDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data! " }); 
            }
      Order? order =await orderRepo.GetById(dto.OrderId);
            if (order is null) {
                return NotFound(new { Message = "There is no Order meet this id!" }); 
            }
            order.OrderStatusID = 12;
            order.ReasonOfReject = dto.Message;  
            order.Rejected=true;
orderRepo.Update(order); 
            await orderRepo.SaveAsync();
            return Ok(new { Message = "Successfully Rejected!" });
        }

    }
}

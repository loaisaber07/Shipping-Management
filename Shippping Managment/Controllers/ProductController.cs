using Data_Access_Layer.DTO.Order;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productRepo;
        private readonly IOrder orderRepo;

        public ProductController(IProduct productRepo,IOrder orderRepo)
        {
            this.productRepo = productRepo;
            this.orderRepo = orderRepo;
        }
        [HttpGet]
        public async Task<ActionResult>GetProductByOrderId(int orderId)
        {
            Order? order = await orderRepo.GetById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            List<Product> list =  await productRepo.getProductsByOrderId(orderId);
            List<GetProductDTO> getProductDTOs = new List<GetProductDTO>();
            foreach (Product product in list)
            {
                GetProductDTO productDTO = new GetProductDTO
                {
                    Id = product.ID,
                    Name = product.Name,
                    ProductWeight = product.Weight,
                    Quantity = product.Quantity,
                    OrderId = product.OrderID

                };
                getProductDTOs.Add(productDTO);
            }
            return Ok(getProductDTOs);
        }
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult> Delete(int id)
        {
            Product? product = await productRepo.GetAsyncById(id);
            if (product is null)
            {
                return NotFound(new {Message="Can't Find product"});
            }
            await productRepo.DeleteAsync(id);
            await productRepo.SaveAsync();
            return Ok(new {Message="Product Deleted"});
        }
    }
}

using Data_Access_Layer.DTO;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfPaymentController : ControllerBase
    {
        private readonly ITypeOfPayment paymentRepo;

        public TypeOfPaymentController(ITypeOfPayment paymentRepo)
        {
            this.paymentRepo = paymentRepo;
        }
        [HttpPost]
        public async Task<ActionResult> AddTypeOfPayment(AddTypeOfPaymentDTO addType)
        {
            bool chick = await paymentRepo.IsExistByName(addType.Name);
            if(chick)
            {
                return BadRequest(new { Message = "Type Is Allready Exist" });
            }
            TypeOfPayment payment = new TypeOfPayment
            {
                Name = addType.Name
            };
            await paymentRepo.CreateAsync(payment);
            await paymentRepo.SaveAsync();
            TypeOfPayment? type= await  paymentRepo.GetByName(addType.Name);
            GetTypeOfPaymentDTO get = new GetTypeOfPaymentDTO
            {
                Id = type.ID,
                Name = type.Name
            };
            return Ok(get);

        }
    }
}

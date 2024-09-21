using Business_Layer.Services;
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

        [HttpGet]
        public async Task<ActionResult> GetPaymentTypes()
        {
          IEnumerable<TypeOfPayment> payment = await paymentRepo.GetAllAsync();
            IEnumerable<GetTypeOfPaymentDTO> DTO =TypeOfPaymentService.GetPaymentList(payment);
            return Ok(DTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddTypeOfPayment(AddTypeOfPaymentDTO addType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data" });
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
        [HttpPut]
        public async Task<ActionResult> EditePaymentTypes(EditPaymentDTO edit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            TypeOfPayment? type = await paymentRepo.GetAsyncById(edit.ID);
            if (type == null)
            {
                return NotFound();
            }
           type.Name = edit.Name;   
            if(!paymentRepo.Update(type))
            {
                return BadRequest(new { Message = "Can Not Save" });
            }
            await paymentRepo.SaveAsync();
            return Ok();



        }
        [HttpDelete("{typeId:int}")]
        public async Task<ActionResult> DeleteType(int typeId)
        {
          TypeOfPayment? type =  await paymentRepo.GetAsyncById(typeId);
            if (type is null)
            {
                return NotFound(new {Message="Type Not Found !!"});  
            }
            await paymentRepo.DeleteAsync(typeId);
            await paymentRepo.SaveAsync();  
            return Ok(new {Message="Type Deleted"});
        }
    }
}

using Business_Layer.Services.TypeOfReceipt;
using Data_Access_Layer.DTO.TypeOfReceipt;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfReceiptController : ControllerBase
    {
        private readonly ITypeOfReceipt typeOfReceiptRepo;

        public TypeOfReceiptController(ITypeOfReceipt typeOfReceiptRepo)
        {
            this.typeOfReceiptRepo = typeOfReceiptRepo;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IEnumerable<TypeOfReceipt> list = await typeOfReceiptRepo.GetAllAsync();
            IEnumerable<GetTypeOfReceiptDTO> get = TypeOfReceiptService.GetTypeOfReceipts(list);
            return Ok(get);
        }
        [HttpPost]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult> Add(AddTypeOfReceiptDTO add)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            TypeOfReceipt? typeOfReceipt = new TypeOfReceipt
            {
                Name = add.Name
            };
            await typeOfReceiptRepo.CreateAsync(typeOfReceipt);
            await typeOfReceiptRepo.SaveAsync();
           TypeOfReceipt? receipt = await typeOfReceiptRepo.GetReceiptByNameAsync(add.Name);
            if (receipt is not null)
            {
                GetTypeOfReceiptDTO get = TypeOfReceiptService.GetType(receipt);
                return Ok(get);
            }
            return BadRequest(new {Message="Can not add try again !!"});
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult> Edit(EditTypeOfReceiptDTO editType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            TypeOfReceipt? type =  await typeOfReceiptRepo.GetAsyncById(editType.ID);
            if (type is null)
            {
                return NotFound(new {Message="Can not find Type"});
            }
            type.Name = editType.Name;
            if (!typeOfReceiptRepo.Update(type))
            {
                return BadRequest(new { Message = "Can not update try again" });
            }
             await typeOfReceiptRepo.SaveAsync();
            GetTypeOfReceiptDTO get = TypeOfReceiptService.GetType(type);
            return Ok(get);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult> Delete(int id)
        {
           TypeOfReceipt? type = await typeOfReceiptRepo.GetAsyncById(id);
            if (type is null)
            {
                return NotFound(new {Message="can not find Type !!"});
            }
            await typeOfReceiptRepo.DeleteAsync(id);    
            await typeOfReceiptRepo.SaveAsync();
            return Ok(new {Message="Type Deleted"});
        }
    }
}

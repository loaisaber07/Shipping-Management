using Business_Layer.Services.TypeOfCharge;
using Data_Access_Layer.DTO;
using Data_Access_Layer.DTO.TypeOfCharge;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfChargeController : ControllerBase
    {
        private readonly ITypeOfCharge chargeRepo;

        public TypeOfChargeController(ITypeOfCharge chargeRepo)
        {
            this.chargeRepo = chargeRepo;
        }
        [HttpPost]
        public async Task<ActionResult> AddTypeOfCharge(AddTypeOfChargeDTO ChargeDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data" });
            }
            TypeOfCharge type = new TypeOfCharge
            {
                Name = ChargeDTO.Name,
                Cost = ChargeDTO.Cost
            };
            await chargeRepo.CreateAsync(type);
            await chargeRepo.SaveAsync();
           TypeOfCharge? charge = await chargeRepo.GetByName(ChargeDTO.Name);
            GetTypeOfChargeDTO get = TypeOfChargeService.getTypeOfChargeDTO(charge);
            return Ok(get);

        }
        [HttpGet]
        public async Task<ActionResult> GetAllTypeOfCharge()
        {
           IEnumerable<TypeOfCharge>? typeOfCharges= await chargeRepo.GetAllAsync();
            IEnumerable<GetTypeOfChargeDTO> get = TypeOfChargeService.getTypeOfChargeDTOs(typeOfCharges);
            return Ok(get);
        }
        [HttpPut]
        public async Task<ActionResult> EditTypeOfCharge(EditTypeOfChargeDTO editTypeOfCharge)
        {
         TypeOfCharge? charge =   await chargeRepo.GetAsyncById(editTypeOfCharge.ID);
            if (charge is null)
            {
                return NotFound(new {Message="Can not find type of charge"});
            }
            charge.Name = editTypeOfCharge.Name;
            charge.Cost = editTypeOfCharge.Cost;
            if (!chargeRepo.Update(charge))
            {
                return BadRequest(new { Message = "Can not update try again" });
            }
            await chargeRepo.SaveAsync();
            GetTypeOfChargeDTO typeOfCharge = TypeOfChargeService.getTypeOfChargeDTO(charge);
            return Ok(typeOfCharge);
        }
        [HttpDelete("{TypeId:int}")]
        public async Task<ActionResult> DeleteTypeOfCharege(int TypeId)
        {
          TypeOfCharge? type =  await chargeRepo.GetAsyncById(TypeId);
            if (type is null)
            {
                return NotFound(new {Message="Type Not Found !!"});
            }
            await chargeRepo.DeleteAsync(TypeId);
            await chargeRepo.SaveAsync();
            return Ok(new {Message="Type Deleted"});
        }
    }
}

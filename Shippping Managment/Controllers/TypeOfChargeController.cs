using Data_Access_Layer.DTO;
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
            bool chick = await chargeRepo.IsExistByName(ChargeDTO.Name);
            if(chick)
            {
                return BadRequest(new {Message="Type Already Exist"});
            }
            TypeOfCharge type = new TypeOfCharge
            {
                Name = ChargeDTO.Name,
                Cost = ChargeDTO.Cost
            };
            await chargeRepo.CreateAsync(type);
            await chargeRepo.SaveAsync();
           TypeOfCharge? charge = await chargeRepo.GetByName(ChargeDTO.Name);
            GetTypeOfChargeDTO get = new GetTypeOfChargeDTO
            {
                Id = charge.ID,
                Name = charge.Name,
                Cost = charge.Cost
            };
            return Ok(get);

        }
    }
}

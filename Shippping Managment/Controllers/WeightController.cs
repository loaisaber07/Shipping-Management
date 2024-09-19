using Business_Layer.Services.Weight;
using Data_Access_Layer.DTO.WeightDTO;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightController : ControllerBase
    {
        private readonly IWeight weightRepo;

        public WeightController(IWeight weightRepo)
        {
            this.weightRepo = weightRepo;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
          IEnumerable<Weight> weightList=  await weightRepo.GetAllAsync();
            IEnumerable<WeightDTO> dto = WeightService.WeightDTO(weightList);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> AddWeightSettings(AddWeightSettingsDTO settingsDTO)
        {
           IEnumerable<Weight> weightList= await weightRepo.GetAllAsync();
            if (weightList.Any())
            {
                return BadRequest(new { Message= "There is default setting already set" });
            }
            Weight weight = WeightService.MappWeight(settingsDTO);
            await weightRepo.CreateAsync(weight);
            await weightRepo.SaveAsync();
            WeightDTO dTO = WeightService.GetWeightDTO(weight);
            return Ok(dTO);
        }
        [HttpDelete("{settingId:int}")]
        public async Task<ActionResult> Delete(int settingId)
        {
            Weight? weight= await weightRepo.GetAsyncById(settingId);
            if (weight is null)
            {
                return NotFound();
            }
            await weightRepo.DeleteAsync(settingId);
            await weightRepo.SaveAsync();
            return Ok();
        }
        [HttpPut]
         public async Task<ActionResult> Edit(WeightDTO dTO)
         {
            Weight? weight = await weightRepo.GetAsyncById(dTO.ID);
            if (weight is null)
            {
                return NotFound();
            }
            weight.DefaultWeight=dTO.DefaultWeight;
            weight.AdditionalWeight=dTO.AdditionalWeight;
            bool chick = weightRepo.Update(weight);
            if (!chick)
            {
                return BadRequest(new { Message = "Can not update try again " });
            }
            await weightRepo.SaveAsync();
            WeightDTO weightDTO = WeightService.GetWeightDTO(weight);
            return Ok(weightDTO);


         }
    }
}

using Business_Layer.Services.TypeOfOffer;
using Data_Access_Layer.DTO.TypeOfOffer;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfOfferController : ControllerBase
    {
        private readonly ITypeOfOffer typeOfOfferRepo;

        public TypeOfOfferController(ITypeOfOffer typeOfOfferRepo)
        {
            this.typeOfOfferRepo = typeOfOfferRepo;
        }
        [HttpPost]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult>AddTypeOfOffer(AddTypeOfOfferDTO offerDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data" });
            }
            TypeOfOffer offer = TypeOfOfferService.TypeOfOfferMapping(offerDTO);
            await typeOfOfferRepo.CreateAsync(offer);
            await typeOfOfferRepo.SaveAsync();
            TypeOfOffer? type = await typeOfOfferRepo.getByNameAsync(offer.Name);
            if (type is null)
            {
                return BadRequest(new {Message="Can 't Add Try Again"});
            }
            GetTypeOfOfferDTO get = TypeOfOfferService.GetTypeOfOfferDTO(type);
            return Ok(get);
        }
        [HttpPut]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult> EditTypeOfOffer(EditTypeOfOfferDTO edit)
        {
           TypeOfOffer? type= await typeOfOfferRepo.GetAsyncById(edit.ID);
            if (type is null)
            {
                return NotFound(new {Message="Can 't Find Type"});
            }
            type.Name = edit.Name;
            if (!typeOfOfferRepo.Update(type))
            {
                return BadRequest(new {Message="Can 't Update Try Again !"});
            }
            await typeOfOfferRepo.SaveAsync();
            GetTypeOfOfferDTO get = TypeOfOfferService.GetTypeOfOfferDTO(type);
            return Ok(get);
        }
        [HttpGet]
        public async Task<ActionResult>GetAllTypeOfOffer()
        {
            IEnumerable<TypeOfOffer> typeOfOffers = await typeOfOfferRepo.GetAllAsync();
            IEnumerable<GetTypeOfOfferDTO> get = TypeOfOfferService.GetTypeOfOfferDTOs(typeOfOffers);
            return Ok(get);
        }
        [HttpDelete("{ID:int}")]
        [Authorize(Policy = "Admin")]

        public async Task<ActionResult>DeleteType(int ID)
        {
            TypeOfOffer? type = await typeOfOfferRepo.GetAsyncById(ID);
            if (type is null)
            {
                return NotFound(new { Message = "Can 't Find Type" });
            }
            await typeOfOfferRepo.DeleteAsync(ID);
            await typeOfOfferRepo.SaveAsync();
            return Ok(new {Message="Type Deleted"});    
        }
    }
}

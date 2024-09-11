using Business_Layer.Services;
using Data_Access_Layer.DTO;
using Data_Access_Layer.DTO.GovernAndCity;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernController : ControllerBase
    {
        private readonly IGovern govern;
        private readonly ICity cityRepo;

        public GovernController(IGovern govern,ICity cityRepo )
        {
            this.govern = govern;
            this.cityRepo = cityRepo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllgovern() {
    IEnumerable<GovernDTO>DTO= await govern.GetGovernWithCities();
            return Ok(DTO);
        
        }
        [HttpPost("AddGovernWithCity")]
        public async Task <ActionResult> AddGovernWithCity(AddGovernWithCities gov) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            bool result = govern.IsExist(gov.Name);
            if (result) {
                return BadRequest(new { Message = "Already Existed!" }); 
            }
            Govern g = new Govern
            {
                Name = gov.Name
            };
            await govern.CreateAsync(g);
            await govern.SaveAsync();  
        g= govern.GetByName(g.Name);
            if (g is null) {
                return BadRequest(new { Message = "Not Add Correctly!" }); 
            }
            List<City> cities = new List<City>();
foreach(var city in gov.cities)
            {
                cities.Add(new City { 
                Name=city.Name,
                GovernID=g.ID , 
                NormalCharge=city.NormalCharge ,
                PickUpCharge=city.PickUpCharge,
                SpecialChargeForSeller=city.SpecialChargeForSeller
                });

            }
       await cityRepo.BulkInsert(cities);
            return Ok(await govern.GetGovernWithCities());
        
        }
    }
}

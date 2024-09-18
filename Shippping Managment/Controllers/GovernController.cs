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
            IEnumerable<Govern> lsit= await govern.GetGovernWithCities();
            IEnumerable<GovernDTO> DTO = lsit.Select(g => new GovernDTO
            {
                ID = g.ID,
                Name = g.Name,
                cities = g.Cities.Select(city => new CityDTO
                {
                    ID = city.ID,
                    Name = city.Name,
                }).ToList()

            });
            return Ok(DTO);
        
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetGovernWithItsCities(int id)
        {
            Govern? gov = await govern.GetByID(id);
            if (gov == null)
            {
                return BadRequest(new { Message = "Not Found!" });
            }

            GovernDTO dto = new()
            {
                ID = gov.ID,
                Name = gov.Name,
                cities = gov.Cities.Select(city => new CityDTO
                {
                    ID = city.ID,
                    Name = city.Name,
                }).ToList()

            };

            return Ok(dto);

        }
        [HttpPost("add")]
        public async Task <ActionResult> AddGovernWithCity(AddGovernWithCities gov) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Govern g = new Govern
            {
                Name = gov.Name
            };
            await govern.CreateAsync(g);
            await govern.SaveAsync();  
            g= await govern.GetByName(g.Name);
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
                });

            }
            await cityRepo.BulkInsert(cities);
            return Ok(await govern.GetGovernWithCities());
        
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteGovernWithItsCities(int governID) { 
             Govern? gov = await govern.GetByID(governID);
            if (gov is null) {
                return BadRequest(new { Message = "No Govern Founded" }); 
            }
            IEnumerable<City> cities=await cityRepo.BulkSelect(governID);
            bool result=    cityRepo.BulkRemove(cities);
            if (!result) {
                return BadRequest(new { Message = "Can't delete Cities" });
            }
            await cityRepo.SaveAsync();
            await govern.DeleteAsync(governID); 
            await govern.SaveAsync();
            return Ok(new { Message = "Successfully Deleted" });  
        }
    }
}

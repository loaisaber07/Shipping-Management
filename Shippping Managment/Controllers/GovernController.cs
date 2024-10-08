﻿using Business_Layer.Services;
using Data_Access_Layer.DTO;
using Data_Access_Layer.DTO.GovernAndCity;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult> GetAllgovern() 
        {
            IEnumerable<GovernDTO>DTO= await govern.GetGovernWithCities();
            return Ok(DTO);
        }

        [HttpGet("{governID:int}")]
        public ActionResult GetGovernWithItsCities(int governID) { 
            Govern? g = govern.GetWithID(governID);
            if (g is null) {
                return NotFound(new { Message = "Govern Not Found" });
            }
            GovernDTO DTO = new()
            {
                ID = g.ID,
                Name = g.Name,
                Status = g.Status,
                cities = g.Cities.Select(city => new CityDTO
                {
                    ID = city.ID,
                    Name = city.Name,
                }).ToList()
            };
            return Ok(DTO);
        }
        [HttpPost("AddGovernWithCity")]
        [Authorize(Policy = "Admin")]

        public async Task <ActionResult> AddGovernWithCity(AddGovernWithCities gov) {
            if (!ModelState.IsValid) {
                return BadRequest();
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
            foreach (var city in gov.cities)
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

        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult>EditGovern(EditGovernDTO edit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "invalid data" });
            }
            Govern? govern1 =await govern.GetAsyncById(edit.ID);
            if (govern1 is null)
            {
                return NotFound(new {Message="Govern Not Found"});
            }
            govern1.Name = edit.Name;
            govern1.Status = edit.Status;
            if (!govern.Update(govern1))
            {
                return BadRequest(new{Message="Failed To Update Try Again" });
            }
          await govern.SaveAsync();
            return Ok(new { Message = "Govren Updated" });

        }

        [HttpDelete("{governID:int}")]
        public async Task<ActionResult> DeleteGovernWithItsCities(int governID) 
        { 
            Govern? gov = govern.GetWithID(governID);
            if (gov is null) {
                return BadRequest(new { Message = "No Govern Founded" }); 
            }
            IEnumerable<City> cities=await cityRepo.BulkSelect(governID);
            bool result=    cityRepo.BulkRemove(cities);
            if (!result) {
                return BadRequest(new { Message = "Can't delete Cities" });
            }
           await cityRepo.SaveAsync();
           await   govern.DeleteAsync(governID); 
           await govern.SaveAsync();
           return Ok(new { Message = "Successfully Deleted" });  
        }
    }
}

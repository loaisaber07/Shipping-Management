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
    public class CityController : ControllerBase
    {
        private readonly ICity cityRepo;

        public CityController(ICity cityRepo)
        {
            this.cityRepo = cityRepo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllCities()
        {
            IEnumerable<City> cities= await cityRepo.GetAllAsync();
            IEnumerable<GetCityDTO> list=CityService.MappingCity(cities);
            return Ok(list);
        }
        [HttpPut]
        public async Task<ActionResult> EditCity(EditCityDTO dto)
        {
            bool check = await cityRepo.IsExistById(dto.Id);
            if (!check)
            {
                return NotFound(new { Message = "City Not Found !!" });
            }
            City city = CityService.EditCity(dto);
            string oldName = await cityRepo.GetNameById(dto.Id);
            if (oldName != null && oldName != dto.Name) //   True => name changed #AKR
            {
                bool checkName = await cityRepo.IsExistByName(dto.Name); // True => the new name that entered is already owned by another city #AKR 
                if (checkName)
                {
                    return BadRequest(new { Message = "There Is A city With The Same Name " });
                }
            }

            if (!cityRepo.Update(city))
            {
                return BadRequest("Failed to update city !!");
            }
            await cityRepo.SaveAsync();
            return Ok(city);
        }
        [HttpPost]
        public async Task<ActionResult> AddCity(AddCityDTO addCity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid Data" });
            }
            City city = CityService.AddCity(addCity);
            await cityRepo.CreateAsync(city);
            await cityRepo.SaveAsync();
            EditCityDTO cityDTO = CityService.Mapping(city);
            return Ok(cityDTO);
        }
        [HttpDelete("{cityId:int}")]
        public async Task<ActionResult> DeleteCity(int cityId)
        {
            bool check =await cityRepo.IsExistById(cityId);
            if (!check)
            {
                return NotFound(new {Message="City not Found"});
            }
            await cityRepo.DeleteAsync(cityId);
            await cityRepo.SaveAsync();
            return Ok(new {Message= "City Deleted Successfully" });
        }
    }
}

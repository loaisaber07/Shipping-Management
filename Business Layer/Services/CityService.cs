using Data_Access_Layer.DTO;
using Data_Access_Layer.DTO.GovernAndCity;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class CityService
    {
        public static  City EditCity(EditCityDTO dto)
        {
            City city = new City()
            {
                ID=dto.Id,
                Name = dto.Name,
                NormalCharge = dto.NormalCharge,
                PickUpCharge = dto.PickUpCharge,
                GovernID=dto.GovernID
            };
            return city;
        }

        public static IEnumerable<GetCityDTO> MappingCity(IEnumerable<City> cityList)
        {
            List<GetCityDTO> list = new List<GetCityDTO>();
            foreach (City city in cityList)
            {
                GetCityDTO dto = new GetCityDTO
                {
                    Name = city.Name,
                    ID = city.ID,
                    NormalCharge = city.NormalCharge,
                    PickUpCharge=city.PickUpCharge,
                     GovernID=city.GovernID
                };
                list.Add(dto);
            }
            return list;

        }
        public static City AddCity(AddCityDTO addCity)
        {
            City city = new City
            {
                Name=addCity.Name,
                GovernID=addCity.GovernID,
                NormalCharge=addCity.NormalCharge,
                PickUpCharge = addCity.PickUpCharge,
            };
            return city;
        }
        public static EditCityDTO Mapping(City city)
        {
            EditCityDTO cityDTO = new EditCityDTO
            {
                Name = city.Name,
                GovernID = city.GovernID,
                NormalCharge = city.NormalCharge,
                PickUpCharge = city.PickUpCharge,
                Id = city.ID,
            };
            return cityDTO;
        }
    }
}

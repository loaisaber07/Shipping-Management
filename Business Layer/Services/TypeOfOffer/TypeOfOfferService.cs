using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.TypeOfOffer
{
    using Data_Access_Layer.DTO.Agent;
    using Data_Access_Layer.DTO.TypeOfOffer;
    using Data_Access_Layer.Entity;
    public class TypeOfOfferService
    {
        public static TypeOfOffer TypeOfOfferMapping(AddTypeOfOfferDTO dto)
        {
            TypeOfOffer offer = new TypeOfOffer
            { 
                Name=dto.Name            
            };
            return offer;
        }
        public static GetTypeOfOfferDTO GetTypeOfOfferDTO(TypeOfOffer offer)
        {
            GetTypeOfOfferDTO get = new GetTypeOfOfferDTO
            {
                ID = offer.ID,
                Name = offer.Name,
            };
            return get;
        }
        public static IEnumerable<GetTypeOfOfferDTO> GetTypeOfOfferDTOs(IEnumerable<TypeOfOffer> types)
        {
            List<GetTypeOfOfferDTO> gets = new List<GetTypeOfOfferDTO>();
            foreach (var type in types)
            {
                GetTypeOfOfferDTO dto = new GetTypeOfOfferDTO
                {
                    ID= type.ID,
                    Name = type.Name,
                };
                gets.Add(dto);
            }
            return gets;
        }
    }
}

using Data_Access_Layer.DTO.WeightDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Weight
{
    using Data_Access_Layer.Entity;
    public class WeightService
    {
        public static Weight MappWeight(AddWeightSettingsDTO settingsDTO)
        {
            Weight weight = new Weight
            {
                AdditionalWeight = settingsDTO.AdditionalWeight,
                DefaultWeight = settingsDTO.DefaultWeight,
            };
            return weight;

        }
        public static IEnumerable<WeightDTO> WeightDTO (IEnumerable<Weight> weight)
        {
            List<WeightDTO> weights = new List<WeightDTO>();
            foreach (var item in weight)
            {
                WeightDTO dto = new WeightDTO
                {
                    ID = item.ID,
                    AdditionalWeight = item.AdditionalWeight,
                    DefaultWeight = item.DefaultWeight,
                };
                weights.Add(dto);
            }
            return weights;

        }
        public static WeightDTO GetWeightDTO(Weight weight)
        {
            WeightDTO dTO = new WeightDTO
            {
                ID = weight.ID,
                AdditionalWeight = weight.AdditionalWeight,
                DefaultWeight = weight.DefaultWeight
            };
            
            return dTO;

        }
    }
}

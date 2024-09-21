using Data_Access_Layer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.TypeOfCharge
{
    using Data_Access_Layer.Entity;
    using System.Runtime.InteropServices;

    public class TypeOfChargeService
    {
        public static IEnumerable<GetTypeOfChargeDTO> getTypeOfChargeDTOs(IEnumerable<TypeOfCharge> typeOfCharges)
        {
            List<GetTypeOfChargeDTO> getTypeOfChargeDTOs = new List<GetTypeOfChargeDTO>();
            foreach (var item in typeOfCharges)
            {
                GetTypeOfChargeDTO dto = new GetTypeOfChargeDTO
                {
                    Cost = item.Cost,
                    Id = item.ID,
                    Name = item.Name
                };
                getTypeOfChargeDTOs.Add(dto);
            }
            return getTypeOfChargeDTOs;
        }
        public static GetTypeOfChargeDTO getTypeOfChargeDTO(TypeOfCharge charge)
        {
            GetTypeOfChargeDTO get = new GetTypeOfChargeDTO
            {
                Id = charge.ID,
                Name = charge.Name,
                Cost = charge.Cost
            };
            return get;
        }
    }
}

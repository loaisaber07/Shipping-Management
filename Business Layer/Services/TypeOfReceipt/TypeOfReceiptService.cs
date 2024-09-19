using Data_Access_Layer.DTO.TypeOfReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.TypeOfReceipt
{
    using Data_Access_Layer.Entity;
    public class TypeOfReceiptService
    {
        public static IEnumerable<GetTypeOfReceiptDTO> GetTypeOfReceipts(IEnumerable<TypeOfReceipt> typeOfReceiptList)
        {
            List<GetTypeOfReceiptDTO> getTypeOfReceiptDTOs = new List<GetTypeOfReceiptDTO>();
            foreach (var item in typeOfReceiptList)
            {
                GetTypeOfReceiptDTO getTypeOfReceiptDTO = new GetTypeOfReceiptDTO
                {
                    ID = item.ID,
                    Name = item.Name,
                };
                getTypeOfReceiptDTOs.Add(getTypeOfReceiptDTO);

            }
            return getTypeOfReceiptDTOs;   
        }
        public static GetTypeOfReceiptDTO GetType(TypeOfReceipt typeOfReceipt)
        {
            GetTypeOfReceiptDTO dto = new GetTypeOfReceiptDTO
            {
                ID = typeOfReceipt.ID,
                Name = typeOfReceipt.Name
            };
            return dto;
        }
    }
}

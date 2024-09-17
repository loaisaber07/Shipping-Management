using Data_Access_Layer.DTO;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
   public class TypeOfPaymentService
    {
        public static IEnumerable<GetTypeOfPaymentDTO> GetPaymentList(IEnumerable<TypeOfPayment> list)
        {
            List<GetTypeOfPaymentDTO> getTypeOfPaymentDTO = new List<GetTypeOfPaymentDTO>();
            foreach (var item in list)
            {
                getTypeOfPaymentDTO.Add(new GetTypeOfPaymentDTO
                {

                    Id = item.ID,
                    Name = item.Name,
                }); 
            }
            return getTypeOfPaymentDTO;
        }
    }
}

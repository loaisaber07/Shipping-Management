using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Agent
{
    public class EditAgentDTO
    {
        public string ID { get; set; }
        [UniqueAgentNameEdit]
        public string UserName { get; set; }
        [UniquePhoneNumberEditing]
        public string phoneNumber { get; set; }
        public bool Status { get; set; }
        public int BranchID { get; set; }
        public int ThePrecentageOfCompanyFromOffer { get; set; }
        public int GovernID { get; set; }
        public int TypeOfOfferID { get; set; }
        public string Address { get; set; }
    }
}

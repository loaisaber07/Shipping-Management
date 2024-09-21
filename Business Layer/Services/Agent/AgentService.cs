using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Agent
{
    using Data_Access_Layer.DTO.Agent;
    using Data_Access_Layer.Entity;
    public class AgentService
    {
        public static Agent GetAgent(AddAgentDTO agentDTO)
        {
            Agent agent = new Agent
            {
                UserName=agentDTO.Name,
                Email=agentDTO.Email,
                PhoneNumber=agentDTO.Phone,
                BranchID=agentDTO.BranchID,
                ThePrecentageOfCompanyFromOffer=agentDTO.ThePrecentageOfCompanyFromOffer,
                GovernID=agentDTO.GovernID,
                TypeOfOfferID=agentDTO.TypeOfOfferID,
            };
            return agent; 
        }
    }
}

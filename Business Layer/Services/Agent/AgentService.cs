using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Agent
{
    using Data_Access_Layer.DTO.Agent;
    using Data_Access_Layer.Entity;
    using Data_Access_Layer.Repositry;

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
                Address = agentDTO.Address
            };
            return agent; 
        }
        public static IEnumerable<GetAgentDTO> getAgentDTOs(IEnumerable<Agent>agents)
        {
            List<GetAgentDTO> gets = new List<GetAgentDTO>();
            foreach (Agent agent in agents)
            {
                GetAgentDTO dTO = new GetAgentDTO
                {
                    ID= agent.Id,
                    Name=agent.UserName,
                    Email= agent.Email,
                    Phone=agent.PhoneNumber,
                    ThePrecentageOfCompanyFromOffer=agent.ThePrecentageOfCompanyFromOffer,
                    GovernID = agent.GovernID,
                    BranchID=agent.BranchID,
                    TypeOfOfferID= agent.TypeOfOfferID, 
                    Address = agent.Address
                };
                gets.Add(dTO);
            }
            return gets;
        }
        public static GetAgentDTO getAgentDTO(Agent agent)
        {
            
            GetAgentDTO agentDTO = new GetAgentDTO
            {
                ID = agent.Id,
                Name = agent.UserName,
                Email = agent.Email,
                Phone = agent.PhoneNumber,
                ThePrecentageOfCompanyFromOffer = agent.ThePrecentageOfCompanyFromOffer,
                GovernID = agent.GovernID,
                BranchID = agent.BranchID,
                TypeOfOfferID = agent.TypeOfOfferID,
                Address = agent.Address
            };
            return agentDTO;
        }
        public static Agent MapAgentForEditing(Agent agent ,EditAgentDTO dto)
        {
            agent.UserName = dto.UserName;
            agent.PhoneNumber=dto.phoneNumber;
            agent.Status = dto.Status;
            agent.BranchID = dto.BranchID;
            agent.ThePrecentageOfCompanyFromOffer=dto.ThePrecentageOfCompanyFromOffer;
            agent.GovernID= dto.GovernID;
            agent.TypeOfOfferID = dto.TypeOfOfferID; 
            agent.Address=dto.Address;
            return agent;
        }
    }
}

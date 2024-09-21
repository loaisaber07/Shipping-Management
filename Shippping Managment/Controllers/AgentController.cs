using Business_Layer.Services.Agent;
using Business_Layer.Services.Employee;
using Data_Access_Layer.DTO.Agent;
using Data_Access_Layer.DTO.Employee;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IUser userRepo;

        public AgentController(IUser userRepo)
        {
            this.userRepo = userRepo;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult>GetAllAgent()
        {
            IEnumerable<Agent> agentList =await userRepo.GetAllAgents();
            IEnumerable<GetAgentDTO> get = AgentService.getAgentDTOs(agentList);
            return Ok(get);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult>DeleteAgent(string id)
        {
            bool chick = await userRepo.DeleteAgentAsync(id);
            if (!chick)
            {
                return BadRequest(new { Message = "Can not Delete Agent" });
            }
            await userRepo.SaveAsync();
            return Ok(new { Message = "Agent Deleted" });
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult>GetAgentById(string id)
        {
            Agent? agent = await userRepo.GetAgentAsyncById(id);
            if (agent is null)
            {
                return NotFound(new { Message = "Can not Find Agent Withe Same Id" });
            }
            GetAgentDTO getAgentDTO = AgentService.getAgentDTO(agent);
            return Ok(getAgentDTO);
        }
        [HttpPut]
        public async Task<ActionResult> EditAgent(EditAgentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "invalid data" });
            }
            ApplicationUser? user = await userRepo.GetAgentAsyncById(dto.ID);
            if (user is null)
            {
                return NotFound(new { Message = "Can't find Agent hava the same id!" });
            }
            user = AgentService.MapAgentForEditing((Agent)user, dto); 
            bool check = await userRepo.UpdateUser(user);
            if (!check)
            {
                return BadRequest(new { Message = "Can't update Agent!" });
            }
            await userRepo.SaveAsync();
            GetAgentDTO getAgentDTO = AgentService.getAgentDTO((Agent)user);
            return Ok(getAgentDTO);
            

        }

    }
}

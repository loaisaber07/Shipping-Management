using Business_Layer.Services;
using Data_Access_Layer.DTO.FieldPrivilege;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldPrivilegeController : ControllerBase
    {
        private readonly IFieldPrivilege fieldprivilegeRepo;
        private readonly IFieldJob fieldRepo;

        public FieldPrivilegeController(IFieldPrivilege fieldprivilegeRepo ,IFieldJob fieldRepo)
        {
            this.fieldprivilegeRepo = fieldprivilegeRepo;
            this.fieldRepo = fieldRepo;
        }
        [HttpGet]
        public IActionResult Get() {
            IEnumerable< GetFieldPrivilegeDTO >DTO=FieldPrivilegeService.GetAllFieldPrivileg(fieldprivilegeRepo.GetAll());
            if (DTO is not null) {
                return Ok(DTO); 
            }
            return NotFound();
        }
        [HttpGet]
        [Route("GetByFJID")]
        public async Task<ActionResult>GetByFieldJobId(int fjId)
        {
             FieldJob? F = await fieldRepo.GetAsyncById(fjId);
            if (F is null)
            {
                return NotFound();
            }
           IEnumerable<FieldPrivilege>FJP = await fieldprivilegeRepo.GetByFJId(fjId);
            IEnumerable<GetFieldPrivilegeDTO> DTO = FieldPrivilegeService.AllFieldPrivileg(FJP);
            return Ok(DTO);
        }
    }
}

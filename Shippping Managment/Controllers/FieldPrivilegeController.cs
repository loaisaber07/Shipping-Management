using Business_Layer.Services;
using Data_Access_Layer.DTO.FieldPrivilege;
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

        public FieldPrivilegeController(IFieldPrivilege fieldprivilegeRepo)
        {
            this.fieldprivilegeRepo = fieldprivilegeRepo;
        }
        [HttpGet]
        public IActionResult Get() {
   IEnumerable< GetFieldPrivilegeDTO >DTO=FieldPrivilegeService.GetAllFieldPrivileg(fieldprivilegeRepo.GetAll());
            if (DTO is not null) {
                return Ok(DTO); 
            }
            return NotFound();
        }
    }
}

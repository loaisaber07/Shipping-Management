using Business_Layer.Services;
using Data_Access_Layer.DTO.FieldJob;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldJobController : ControllerBase
    {
        private readonly IFieldJob fieldRepo;
        private readonly IFieldPrivilege fieldprivilegeRepo;

        public FieldJobController(IFieldJob fieldRepo , IFieldPrivilege fieldprivilegeRepo)
        {
            this.fieldRepo = fieldRepo;
            this.fieldprivilegeRepo = fieldprivilegeRepo;
        }
        [HttpGet]
        public ActionResult Get() {
            IEnumerable<FieldJobDTO> dto = fieldRepo.GetAllFieldWithPrivilege();
            return Ok(dto); 
        
        }
        [HttpPost]
        public async Task<ActionResult> AddField(AddFieldJob addFieldJob) {
            if (!ModelState.IsValid) {
                return BadRequest(new { Message ="Inavlid sent Object something is missed! " }); 
            }

            bool result =  fieldRepo.IsExist(addFieldJob.Name);
            if (result)
            {
                return BadRequest(new {Message="Field is allready exsit"});  
            }
            FieldJob? b = new FieldJob {
                Name = addFieldJob.Name };
         await  fieldRepo.CreateAsync(b);
         await fieldRepo.SaveAsync();
        b= fieldRepo.GetByName(addFieldJob.Name);
            if (b is null) {
                return NotFound(new { Message="Field to save Fieldjob!"});
            }
 IEnumerable<FieldPrivilege>FB= FieldPrivilegeService.CreateListOfFieldPrivilege(b.ID, addFieldJob.FieldPrivilegeDTo);
          await  fieldprivilegeRepo.BulkInsert(FB);
          await  fieldprivilegeRepo.SaveAsync();
            return RedirectToAction("Get");


        }
    }
}

using Business_Layer.Services;
using Business_Layer.Services.FieldJob;
using Data_Access_Layer.DTO.FieldJob;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldJobController : ControllerBase
    {
        private readonly IFieldJob fieldRepo;
        private readonly IFieldPrivilege fieldprivilegeRepo;
        private readonly IPrivilege privilegeRepo;

        public FieldJobController(IFieldJob fieldRepo , IFieldPrivilege fieldprivilegeRepo , 
            IPrivilege privilegeRepo
            )
        {
            this.fieldRepo = fieldRepo;
            this.fieldprivilegeRepo = fieldprivilegeRepo;
            this.privilegeRepo = privilegeRepo;
        }
        [HttpGet]
        public ActionResult Get() {
            IEnumerable<FieldJobDTO> dto = fieldRepo.GetAllFieldWithPrivilege();
            return Ok(dto); 
        
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult>GetFieldJobWithPrivileges(int id)
        {
            FieldJob? fieldJob =await fieldRepo.GetFieldJobById(id);
            if (fieldJob is null)
            {
                return NotFound(new {Message="Field Not Found !!"});
            }
            FieldJobDTO fieldJobDTO = FieldJobService.MappingFieldJob(fieldJob);
           return Ok(fieldJobDTO);

        }
        [HttpPost]
        [Authorize(Policy = "Admin")]
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
      IEnumerable<int> privilegeIds = await privilegeRepo.GetPrivilegeIds();
 IEnumerable<FieldPrivilege>FB= FieldPrivilegeService.MappingAddFieldJob(b.ID,privilegeIds );
          await  fieldprivilegeRepo.BulkInsert(FB);
          await  fieldprivilegeRepo.SaveAsync();
            return RedirectToAction("Get");


        }
        [HttpPut]
        public async Task<ActionResult> EditPermissionForField(EditFieldPrivilege obj) {

            FieldJob? fieldResult = await  fieldRepo.GetAsyncById(obj.ID);
            if (fieldResult is null) {
                return NotFound(new { Message = "Can't find this fieldJob!" }); 
            } 
            fieldResult.Name = obj.Name;
            if (!fieldRepo.Update(fieldResult)) {
                return BadRequest(new { Message = "Can't update! Try again" }); 
            }
            await fieldRepo.SaveAsync();
            IEnumerable<FieldPrivilege> fieldPriviege = FieldPrivilegeService.EditListOfFieldPrivilege(obj);
            if (!fieldprivilegeRepo.BulkIUpdate(fieldPriviege)) {
                return BadRequest(new { Message = "Can't update !!!" }); 
            }
            await fieldRepo.SaveAsync();
            await fieldprivilegeRepo.SaveAsync();
            return Ok(new { Message="Update Successfully!"}); 
        
        }
        [HttpDelete("{id:int}")]
        public async  Task<ActionResult> Delete(int id) {
            
           if(! await fieldRepo.IsExistByIdAsync(id))
            {
                return NotFound(new { Messsage = "id not found" });
            } 
            bool check=  await  fieldprivilegeRepo.BulkDelte(id);
            if (!check)
            {
                return BadRequest(new { Message = "Can't delete try again" }); 

            }  

            await fieldRepo.DeleteAsync(id);
            await fieldRepo.SaveAsync();
            return Ok(new { Message = "Deleted successfully" });
        
        }
    }
}

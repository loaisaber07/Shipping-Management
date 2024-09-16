using Business_Layer.Services;
using Business_Layer.Services.Privileges;
using Data_Access_Layer.DTO.Privilege;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivilegeController : ControllerBase
    {
        private readonly IPrivilege privilegeRepo;
        private readonly IFieldJob fieldRepo;
        private readonly IFieldPrivilege fpRepo;

        public PrivilegeController(IPrivilege privilegeRepo  ,IFieldJob fieldRepo ,
            IFieldPrivilege FpRepo)
        {
            this.privilegeRepo = privilegeRepo;
            this.fieldRepo = fieldRepo;
            fpRepo = FpRepo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllPrivilege()
        {
            IEnumerable<Privilege> privilege= await privilegeRepo.GetAllAsync();
            IEnumerable<EditPrivilegeDTO> dto = Privilege_Service.GetPrivileges(privilege);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> AddPrivilege(AddPrivilegeDTO addPrivilege)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
          bool check = await privilegeRepo.IsExsitsByName(addPrivilege.Name);
            if (check)
            {
                return BadRequest(new { Message = "There Is A privilege Whth The Same Name" });
            }
            Privilege privilege = Privilege_Service.AddPrivilege(addPrivilege);
            await privilegeRepo.CreateAsync(privilege);
            await privilegeRepo.SaveAsync();
            
        privilege=  await privilegeRepo.GetByName(addPrivilege.Name);
            IEnumerable<FieldJob> f = await fieldRepo.GetAllAsync();
            if(f is not null)
            {

     IEnumerable<FieldPrivilege> fb= PrivilegeService.MappingFieldJob(privilege, f);
           await fpRepo.BulkInsert(fb);
           await fpRepo.SaveAsync();

            }
            EditPrivilegeDTO editPrivilegeDTO =Privilege_Service.GetPrivilege(privilege);
            return Ok(editPrivilegeDTO);
        }

        [HttpPut]
        public async Task<ActionResult> EditPrivilege(EditPrivilegeDTO editPrivilege)
        {
            bool chick = await privilegeRepo.IsExsitsById(editPrivilege.ID);
            if (!chick)
            {
                return NotFound(new { Message = "Privilege Not Found !!" });
            }
            Privilege privilege = Privilege_Service.EditPrivilege(editPrivilege);
            string oldName = await privilegeRepo.GetNameById(editPrivilege.ID);
            if (oldName != editPrivilege.Name && oldName is not null)
            {
                chick= await privilegeRepo.IsExsitsByName(editPrivilege.Name);
                if (chick)
                {
                    return BadRequest(new { Message = "The Name You Entered Is Allready Exist" });
                }
            }
            if (!privilegeRepo.Update(privilege))
            {
                return BadRequest(new { Message = " Failed To Update Privilege" });
            }
            await privilegeRepo.SaveAsync();
            return Ok(new {Message ="Privilege Updated Successfully"});
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePrivilege(int privilegeId)
        {
            bool chick = await privilegeRepo.IsExsitsById(privilegeId);
            if (!chick)
            {
                return NotFound(new { Message="Privilege Not Found !!" });
            }
            await privilegeRepo.DeleteAsync(privilegeId);
            await privilegeRepo.SaveAsync();
            return Ok();
        }
    }
}

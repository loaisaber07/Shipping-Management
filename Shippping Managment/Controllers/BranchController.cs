using Business_Layer.Services;
using Data_Access_Layer.DTO.BatchDTO;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranch branchRepo;

        public BranchController(IBranch branchRepo ,ShippingDataBase context)
        {
            this.branchRepo = branchRepo;
        }
        [HttpGet]
        public ActionResult Get() {

          IEnumerable<BranchDTO>dto=branchRepo.GetAll();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> AddBranch(AddBranchDTO addBranch)
        {
            bool result = branchRepo.IsExist(addBranch.Name);
            if (!result)
            {

                Branch branch = new Branch { Name = addBranch.Name  };
                await branchRepo.CreateAsync(branch);
                await branchRepo.SaveAsync(); 
               
          BranchDTO? dto=branchRepo.GetByName(addBranch.Name);
                if (dto is null) {
                    return BadRequest(new { Message = "Not Added Successfully" }); 
                }
                return Ok(dto);
            }
            return BadRequest(new { Message = "Branch Allready Exsit" });
        }
        [HttpPut]
        public async Task<ActionResult> Edit(EditBranchDTO dto)
        {
           Branch? branch= await branchRepo.GetAsyncById(dto.ID);
            if (branch is null)
            {
                return NotFound(new {Message="Can not find Branch !"});
            }
            branch.Name = dto.Name;
            branch.Status = dto.Status;
            if (!branchRepo.Update(branch))
            {
                return BadRequest(new {Message="Can not update try again"});
            }
            await branchRepo.SaveAsync();
            BranchDTO branchDTO = BranchService.GetBranchDTO(branch);


            return Ok(dto);
        }



        [HttpDelete]
        public async Task<ActionResult> Delete(int branchId)
        {
           Branch? branch = await branchRepo.GetAsyncById(branchId);
            if (branch is null)
            {
                return NotFound(new { Message = "Can not find branch" });
            }
            await branchRepo.DeleteAsync(branchId);
            await branchRepo.SaveAsync();
            return Ok(new { Message = "Branch Deleted"});
        }

    }
}

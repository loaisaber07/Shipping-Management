using Data_Access_Layer.DTO.BatchDTO;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class BranchService
    {
        public static BranchDTO GetBranchDTO(Branch branch)
        {
            BranchDTO dto = new BranchDTO
            {
                Date = DateTime.Now,
                ID = branch.ID,
                Name = branch.Name,
                Status = branch.Status,
            };
            return dto;
        }
    }
}

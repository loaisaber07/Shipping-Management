using Business_Layer.DTO.Employee;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Employee
{
    public  class EmployeeServices
    { 
        public static ApplicationUser GetEmployee(AddEmployeeDTO dto) { 
        ApplicationUser user = new ApplicationUser { 
        Email = dto.Email,
        PhoneNumber=dto.Phone,
        UserName=dto.Name, 
        BranchID=dto.BranchID,
        FiledJobID=dto.FieldJobID,
        Status=dto.Status, 
        Govern= dto.Govern,
        City=dto.City
        };
            return user;
        
        
        }
    }
}

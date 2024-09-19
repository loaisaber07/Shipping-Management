using Business_Layer.DTO.Employee;
using Data_Access_Layer.DTO.Employee;
using Data_Access_Layer.Entity;
using Microsoft.EntityFrameworkCore;
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

        public static IEnumerable<DisplayEmployeeDTO> GetEmployees(IEnumerable<ApplicationUser> users) {
        return  users.Select(s => new DisplayEmployeeDTO
            {Id = s.Id,
                UserName=s.UserName , 
                Email=s.Email,
                PhoneNumber=s.PhoneNumber,
                FieldJobName=s.FieldJob.Name, 
                BranchName=s.Branch.Name
            }).ToList();
        
        
        }

        public static ApplicationUser MapEmployeeForEditing(ApplicationUser user, EditEmployeeDTO dto) {
            user.FieldJob.ID = dto.FieldJobId;   
            user.Branch.ID = dto.BranchId;
            user.Govern = dto.GovernName;
            user.PhoneNumber = dto.phoneNumber;
            user.City = dto.CityName;
            user.Status = dto.Status;
            user.UserName = dto.UserName;
            return user; 
        }

        public static DisplayEmployeeDTO MapEmployeeforDisplay(ApplicationUser user) {

            DisplayEmployeeDTO dto = new DisplayEmployeeDTO
            {
                BranchName = user.Branch.Name,
                FieldJobName = user.FieldJob.Name,
                Email=user.Email,
                Id=user.Id , 
                PhoneNumber=user.PhoneNumber , 
                UserName=user.UserName
            };
            return dto; 
        }
    }
}

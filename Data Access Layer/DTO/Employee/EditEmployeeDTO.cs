using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Employee
{
    public  class EditEmployeeDTO
    {
        public string ID { get; set; }
        [UniqueUserNameEdit]
        public string UserName { get; set; }
        public int FieldJobId { get; set; }
        public int BranchId { get; set; }
        public string phoneNumber { get; set; }
        [UniquePhoneNumberEditing]
        public string GovernName { get; set; }
        public string CityName { get; set; }
        public bool Status { get; set; }
    }
}

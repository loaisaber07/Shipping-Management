using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Employee
{
    public class DisplayEmployeeDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email{ get; set; }
        public string PhoneNumber { get; set; }
        public string BranchName { get; set; }
        public string FieldJobName { get; set; }
    }
}

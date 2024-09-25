using Data_Access_Layer.Custom_Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class ApplicationUser : IdentityUser
    {
        [Required]

        public override string UserName { get; set; }
        [RegularExpression(@"^01(0|1|2|5)\d{8}$", ErrorMessage = "Invalid phone number")]
        [Required]
        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        [StringLength(maximumLength:100)]
        public string? Address { get; set; }
        public string? Govern { get; set; }
        public string? City { get; set; }
        public bool Status { get; set; } = true; 

        #region mappiing relation between this and field Job 
        public int? FiledJobID { get; set; }
        public virtual FieldJob? FieldJob { get; set; }

        #endregion

        #region mapping the relation between employee and branch 
        [ForeignKey("Branch")]
        public int? BranchID { get; set; }
        [ForeignKey("BranchID")]

        public virtual Branch Branch { get; set; }
        #endregion

    }
}

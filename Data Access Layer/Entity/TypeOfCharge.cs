﻿using Data_Access_Layer.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class TypeOfCharge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        //[UniqueTypeOfCharge]
        public string Name { get; set; }
        [Required]
        public int Cost { get; set; }

        #region mapping the relation between this and product s
        [InverseProperty("TypeOfCharge")]
        public virtual ICollection<Order> Orders { get; set; }
        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class TypeOfOffer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        #region  
        [ForeignKey("Agent")]
        public int AgentID { get; set; }
        [ForeignKey("AgentID")] 
        public virtual Agent Agent { get; set; }
        #endregion
    }
}

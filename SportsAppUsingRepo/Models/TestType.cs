﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAppUsingRepo.Models
{
    public class TestType
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        public virtual ICollection<TestTypeMapper> TestTypeMapper { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Status
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }

        [Required]
        public String Description { get; set; }
    }
}

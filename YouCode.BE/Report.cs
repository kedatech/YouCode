﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BE
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int IdUser { get; set; }
     
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [NotMapped]
        public User User = new User();

    }
}

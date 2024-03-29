﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BE
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Path { get; set; } = string.Empty;
        [ForeignKey("Post")]
        public int IdPost { get; set; }
        
        [NotMapped]
        public Post Post { get; set; } = new Post();

    }
}

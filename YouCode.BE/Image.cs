using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BE
{
    public class Image
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Path { get; set; } = string.Empty;

        [ForeignKey("Post")]
        public int IdPost { get; set; }
        [NotMapped]
        public Post Post { get; set; } = new Post();
    }
}

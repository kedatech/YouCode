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
        public string Id { get; set; }
        [Required]
        public string Path { get; set; }

        [ForeignKey("Post")] 

        public int IdPost { get; set; }
        [NotMapped]
        public Post Post { get; set; } = new Post();
        [NotMapped]
        public int Top_Aux { get; set; }
        public int IdAdd { get; set; }
    }
}

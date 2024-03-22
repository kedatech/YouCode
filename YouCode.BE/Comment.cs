using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BE
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content  { get; set; } = string.Empty;

        [ForeignKey("User")]
        public int IdUser { get; set; }

        [ForeignKey("Post")]
        public int IdPost { get; set; }
        [NotMapped]
        public User User { get; set; } = new User();
        [NotMapped]
        public Post Post { get; set; } =new Post();

    }
}

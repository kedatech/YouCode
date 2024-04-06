using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YouCode.BE
{
    public class Reaction
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int IdUser { get; set; }
        [ForeignKey("Post")]
        public int? IdPost { get; set;  }
        [ForeignKey("Comment")]
        public int? IdComment { get; set; }

        [NotMapped]
        public User User = new User();

        [NotMapped]
         public Post Post = new Post();

        [NotMapped]
         public Comment Comment = new Comment();

    }
}

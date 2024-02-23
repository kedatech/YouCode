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
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int IdUser { get; set; }
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public User User = new User();
    }
}

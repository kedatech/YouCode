using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BE
{
   public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; }
        [ForeignKey("User")]
        public int IdUser { get; set; }
        [NotMapped]
        public User User {  get; set; } = new User();
        public int Top_Aux { get; set; }

    }
}

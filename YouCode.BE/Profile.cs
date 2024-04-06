using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BE
{
    public class Profile
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public string AvatarUrl { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        
        public User User { get; set; }
    }
}

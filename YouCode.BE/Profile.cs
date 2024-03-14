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
        public string MoreInfo { get; set; } = string.Empty;

        public string AvatarUrl { get; set; }

        public string Bio { get; set; }

        public string Email { get; set; }

        [NotMapped]
        public User User { get; set; } = new User ();
    }
}

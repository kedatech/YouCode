using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BE
{
    public class Follower
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int IdFollow { get; set; }
        [ForeignKey("User")]
        public int IdFollower { get; set; }
        public DateTime FollowedAt { get; set; }

        [NotMapped]
        public User User = new  User();
        public int Top_Aux { get; set; }
        


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BE
{
    public class Config
    {
        [Key]
        public int Id { get; set;  }
        [ForeignKey("User")]
        public int IdUser { get; set;  }
        public string Info { get; set;  }

        [NotMapped]
        public User User = new User();
    }
}

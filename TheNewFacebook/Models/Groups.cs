using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewFacebook.Models
{
   public class Groups
    {   
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
        
        
    }
}

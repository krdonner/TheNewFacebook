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

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Information { get; set; }

        [Required]
        public string Image { get; set; }

        public ICollection<Users> Users { get; set; }

    }
}

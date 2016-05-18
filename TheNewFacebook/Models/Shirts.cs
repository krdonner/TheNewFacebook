using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewFacebook.Models
{
    public class Shirts
    {   
        public int Id { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Logo { get; set; }
        public string ImagePath { get; set; }
    }
}

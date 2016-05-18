using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewFacebook.Models
{
    public class ShirtsViewModel
    {
        public IEnumerable<Shirts> Shirts { get; set; }


        public ShirtsViewModel()
        {
            this.Shirts = Shirts;
            

        }
    }
}

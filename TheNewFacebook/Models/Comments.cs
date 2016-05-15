using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewFacebook.Models
{
   public class Comments
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }


        public Users User { get; set; }
        public NewsFeed Newsfeed { get; set; }
    }
}

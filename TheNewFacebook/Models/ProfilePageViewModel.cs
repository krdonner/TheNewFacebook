using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewFacebook.Models
{
    public class ProfilePageViewModel
    {
        public IEnumerable<NewsFeed> NewsFeed { get; set; }
        public IEnumerable<Users> User { get; set; }


        public ProfilePageViewModel()
        {
            this.NewsFeed = NewsFeed;
            this.User = User;

        }
    }


}

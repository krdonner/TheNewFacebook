using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewFacebook.Models
{
    public class GroupsViewModel
    {
        public IEnumerable<Groups> Group { get; set; }
        public IEnumerable<NewsFeed> NewsFeed { get; set; }
        public ICollection<Users> User { get; set; }
       

        public GroupsViewModel() {
            this.User = User;
            this.NewsFeed = NewsFeed;
            this.Group = Group;
            }
    }
}

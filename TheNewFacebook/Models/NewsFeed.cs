﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewFacebook.Models
{
    public class NewsFeed
    {
        public int ID { get; set; }
        public string text { get; set; }
        public DateTime updateDate { get; set; }
        public int likes { get; set; }
        public string Author { get; set; }
        public int UserID { get; set; }
        public string Type { get; set; }
        public string GroupName { get; set; }

       //public User User { get; set; }

        
    }
}

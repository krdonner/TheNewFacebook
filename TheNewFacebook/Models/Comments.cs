﻿using System;
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
        public int NewsFeedId { get; set; }
        public int userId { get; set; }
        public DateTime Updatedate { get; set; }



    }
}

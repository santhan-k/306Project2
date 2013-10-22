using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace CCF2.Models
{
    public class TweetModel
    {
        public string ScreenName { get; set; }

        public string UserName { get; set; }

        public string Image { get; set; }

        public string Text { get; set; }

        public string PublicationDate { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApplication.Models
{
    public class TweetModel
    {
         public int Id { get; set; }
         public int UserId {get; set;}
         public string UserName { get; set;}
        public string Tweets { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApplication.Models
{
    public class UserTweetVM
    {
         public int UserId {get; set;}
         public string UserName { get; set;}
        public string Tweets { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMvc5.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
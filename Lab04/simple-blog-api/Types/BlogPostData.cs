using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_blog_api.Types
{
    public class BlogPostData
    {
        public int id { get; set; }
        public int userId { get; set; }
        public String title { get; set; }
        public String contents { get; set; }
    }
}

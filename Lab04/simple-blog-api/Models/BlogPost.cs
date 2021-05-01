using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_blog_api.Models
{
    public class BlogPost
    {

        public int Id { get; set; }

        public int AuthorId { get; set; }

        public String Title { get; set; }
        public String Contents { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public BlogPost()
        {
            DateTime currentDateTime = DateTime.Now;

            PublishedAt = currentDateTime;
            UpdatedAt = currentDateTime;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_blog_api.Models
{
    public class User
    {
        public int Id { get; set; }

        public String FullName { get; set; }

        public String Email { get; set; }
    }
}

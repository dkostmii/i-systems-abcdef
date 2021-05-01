using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using simple_blog_api.Models;
using Newtonsoft.Json;
using System.IO;

using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;

using simple_blog_api.Types;
using static simple_blog_api.Types.ErrorMessage;

namespace simple_blog_api.Controllers
{
    [ApiController]
    [Route("BlogPosts")]
    public class BlogPostController : Controller
    {
        private List<BlogPost> blogPosts;
        private List<User> users;

        public BlogPostController()
        {
            try
            {
                LoadFakePosts();
                LoadFakeUsers();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

        void LoadFakePosts()
        {
            blogPosts = new List<BlogPost>();
            String dir = Directory.GetCurrentDirectory();
            using (StreamReader r = new StreamReader(dir + "\\Models\\Datasets\\BlogPosts.json"))
            {
                string json = r.ReadToEnd();
                List<BlogPostData> blogPostData = JsonConvert.DeserializeObject<List<BlogPostData>>(json);

                foreach (var item in blogPostData)
                {
                    blogPosts.Add(new BlogPost() { Id = item.id, AuthorId = item.userId, Title = item.title, Contents = item.contents });
                }
            }
        }

        void LoadFakeUsers()
        {
            users = new List<User>();
            String dir = Directory.GetCurrentDirectory();
            using (StreamReader r = new StreamReader(dir + "\\Models\\Datasets\\Users.json"))
            {
                string json = r.ReadToEnd();
                List<UserData> blogPostData = JsonConvert.DeserializeObject<List<UserData>>(json);

                foreach (var item in blogPostData)
                {
                    users.Add(new User() { Id = item.id, FullName = item.fullName, Email = item.email });
                }
            }
        }

        [HttpPost]
        public JsonResult Post([FromBody]BlogPostData blogPostData)
        {
            User found = users.Find(item => item.Id == blogPostData.userId);
            if (found != null)
            {
                int nextId = blogPosts.Count();
                BlogPost result = new BlogPost { Id = nextId, AuthorId = found.Id, Title = blogPostData.title, Contents = blogPostData.contents };
                blogPosts.Add(result);

                return Json(result);
            }
            return PrepareErrorResponse("Author Not Found", HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult Get(int id)
        {
            BlogPost found = blogPosts.Find(item => item.Id == id);
            if (found != null)
            {
                return Json(found);
            }
            return PrepareErrorResponse("Not Found", HttpStatusCode.NotFound);
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(blogPosts);
        }

        
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using simple_blog_api.Models;
using simple_blog_api.Repositories;

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
        private BlogPostsRepository blogPostsRepository;
        private UsersRepository usersRepository;

        public BlogPostController(BlogPostsRepository blogPostsRepository, UsersRepository usersRepository)
        {
            this.blogPostsRepository = blogPostsRepository;
            this.usersRepository = usersRepository;
        }

        [HttpPost]
        public JsonResult Post([FromBody]BlogPostData blogPostData)
        {
            User found = usersRepository.GetUserById(blogPostData.userId);
            if (found != null)
            {
                BlogPost result = blogPostsRepository.InsertBlogPost
                (
                    new BlogPost 
                    { 
                        AuthorId = found.Id,
                        Title = blogPostData.title,
                        Contents = blogPostData.contents 
                    }
                );

                return Json(result);
            }
            return PrepareErrorResponse("Author Not Found", HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult Get(int id)
        {
            BlogPost found = blogPostsRepository.GetBlogPostById(id);
            if (found != null)
            {
                return Json(found);
            }
            return PrepareErrorResponse("Not Found", HttpStatusCode.NotFound);
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(blogPostsRepository.GetAllPosts());
        }
    }
}

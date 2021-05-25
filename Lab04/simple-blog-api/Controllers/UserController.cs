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
using simple_blog_api.Types;
using static simple_blog_api.Types.ErrorMessage;

using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;


namespace simple_blog_api.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : Controller
    {
        private UsersRepository usersRepository; 

        public UserController(UsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [HttpPost]
        public JsonResult Post([FromBody] UserData userData)
        {
            List<User> users = usersRepository.GetAllUsers();
            User found = users.Find(item => item.Email == userData.email);
            if (found == null)
            {
                User result = usersRepository.InsertUser
                (
                    new User 
                    { 
                        FullName = userData.fullName, 
                        Email = userData.email 
                    }
                );

                return Json(result);
            }
            return PrepareErrorResponse($"User with email {userData.email} already exists!", HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult Get(int id)
        {
            User found = usersRepository.GetUserById(id);
            if (found != null)
            {
                return Json(found);
            }
            return PrepareErrorResponse("Not Found", HttpStatusCode.NotFound);
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(usersRepository.GetAllUsers());
        }
    }
}

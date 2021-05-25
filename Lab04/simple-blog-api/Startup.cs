using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using simple_blog_api.Models;
using simple_blog_api.Types;
using simple_blog_api.Repositories;
using Newtonsoft.Json;
using System.IO;

namespace simple_blog_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        List<BlogPost> LoadFakePosts()
        {
            var blogPosts = new List<BlogPost>();
            String dir = Directory.GetCurrentDirectory();
            using (StreamReader r = new StreamReader(dir + "\\Datasets\\BlogPosts.json"))
            {
                string json = r.ReadToEnd();
                List<BlogPostData> blogPostData = JsonConvert.DeserializeObject<List<BlogPostData>>(json);

                foreach (var item in blogPostData)
                {
                    blogPosts.Add(new BlogPost() { Id = item.id, AuthorId = item.userId, Title = item.title, Contents = item.contents });
                }
            }

            return blogPosts;
        }

        List<User> LoadFakeUsers()
        {
            var users = new List<User>();
            String dir = Directory.GetCurrentDirectory();
            using (StreamReader r = new StreamReader(dir + "\\Datasets\\Users.json"))
            {
                string json = r.ReadToEnd();
                List<UserData> blogPostData = JsonConvert.DeserializeObject<List<UserData>>(json);

                foreach (var item in blogPostData)
                {
                    users.Add(new User() { Id = item.id, FullName = item.fullName, Email = item.email });
                }
            }

            return users;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSingleton<BlogPostsRepository>(provider => new BlogPostsRepository(LoadFakePosts()));
            services.AddSingleton<UsersRepository>(provider => new UsersRepository(LoadFakeUsers()));

            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "simple_blog_api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "simple_blog_api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

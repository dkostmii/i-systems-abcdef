using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using simple_blog_api.Models;

#nullable enable

namespace simple_blog_api.Repositories
{
    public class BlogPostsRepository
    {
        private List<BlogPost> blogPosts;
        private int nextId;

        public BlogPostsRepository(List<BlogPost> blogPosts)
        {
            this.blogPosts = blogPosts;
            SetNextId();
        }

        private void SetNextId()
        {
            this.nextId = blogPosts.Count;
        }

        public List<BlogPost> GetAllPosts() => blogPosts;
        public BlogPost? GetBlogPostById(int id) => blogPosts.Find(item => item.Id == id);

        public BlogPost? InsertBlogPost(BlogPost blogPost)
        {
            blogPost.Id = nextId;
            SetNextId();
            blogPosts.Add(blogPost);
            return blogPost;
        }

        public void DeleteBlogPost(int id) => blogPosts = blogPosts.Where(record => record.Id != id).ToList();
    }
}

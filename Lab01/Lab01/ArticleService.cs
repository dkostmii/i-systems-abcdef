using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class ArticleService
    {
        // TODO: Services as a Signletons
        /* Make this service behave like Singleton pattern
         * by creating instance field
         * also make thread blocking for proper async stuff
         */

        List<Article> data;

        ArticleService()
        {
            data = new List<Article>();
            Init();
        }

        private void Init()
        {
            // TODO: Mock articles here
            // Example:
            // Article item = new Article(0)
            // {
            //      Title = "Hello, World!",
            //      Content = "This is the content"
            // };
        }

        public List<Article> ListArticles()
        {
            // TODO: return the list of Articles here
        }

        public void CreateArticle(String title, String content)
        {
            // TODO: Create an Article object here and append it to data
        }

        public void DeleteArticle(int ArticleId)
        {
            // TODO: Delete an Article with ArticleId from data here
        }

        public void UpdateArticle(int ArticleId, String title?, String content?)
        {
            // TODO: Update an article with ArticleId with both title or content optional
        }
    }
}

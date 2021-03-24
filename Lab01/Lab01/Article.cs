using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class Article
    {
        private int article_id;
        private String title;
        private String content;
        private DateTime published_at;
        private DateTime updated_at;
        private int views;

        public Article(int Id)
        {
            this.article_id = Id;
            this.published_at = new DateTime();
            this.updated_at = new DateTime();
            this.views = 0;
        }

        public DateTime UpdatedAt
        {
            get
            {
                return this.updated_at;
            }
            set
            {
                this.updated_at = value;
            }
        }

        public String Title
        {
            get
            {
                return this.title;
            }
            set
            {
                // set the title
                if (value.Length > 10 && value.Length < 96)
                {
                    this.title = value;
                }
            }
        }

        public String Content
        {
            get
            {
                return this.content;
            }
            set
            {
                if (value.Length > 36 && value.Length < 600)
                {
                    this.content = value;
                }
            }
        }

        public override String ToString()
        {
            return "Updated at: " + UpdatedAt.ToString() + "\n\n" +
                Title + "\n\n\n" + Content + "\n\n" + Views;
        }

        public int Views
        {
            get
            {
                return this.views;
            }
        }

        public void IncreaseViews()
        {
            this.views += 1;
        }
    }

    
}

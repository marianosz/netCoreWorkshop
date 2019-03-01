using netCoreWorkshop.Entities;
using System.Collections.Generic;
using System.Linq;

namespace netCoreWorkshop.Business
{
    public class ArticlesService : IArticlesService
    {
        public List<Article> GetAllArticles() => Article.DataSource;

        public Article GetOneArticle(int id) => Article.DataSource.Where(m => m.Id == id).FirstOrDefault();

        public Article AddArticle(Article article)
        {
            article.Id = GetAllArticles().Count() + 1;
            Article.DataSource.Add(article);

            return article;
        }

        public Article EditArticle(int id, Article article)
        {
            if (id != article.Id)
            {
                return null;
            }

            var currentArticle = GetOneArticle(id);

            if (currentArticle == null)
            {
                return null;
            }

            currentArticle.Title = article.Title;

            return currentArticle;
        }

        public void DeleteArticle(int id)
        {
            var article = GetOneArticle(id);

            Article.DataSource.Remove(article);
        }
    }
}
using Microsoft.Extensions.Logging;
using netCoreWorkshop.Data;
using netCoreWorkshop.Entities;
using System.Collections.Generic;
using System.Linq;

namespace netCoreWorkshop.Business
{
    public class ArticlesService : IArticlesService
    {
        private readonly ArticlesContext _context;
        private readonly ILogger<ArticlesService> _logger;

        public ArticlesService(ArticlesContext context, ILogger<ArticlesService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Article> GetAllArticles() => _context.Set<Article>().ToList();

        public Article GetOneArticle(int id)
        {
            var article = _context.Articles.SingleOrDefault(m => m.Id == id);

            return article;
        }

        public Article AddArticle(Article article)
        {
            _logger.LogDebug("Starting save");

            var newArticle = new Article { Title = article.Title };

            _context.Articles.Add(newArticle);
            _context.SaveChanges();

            _logger.LogDebug("Finished save");

            return newArticle;
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

            _context.SaveChanges();

            return currentArticle;
        }

        public void DeleteArticle(int id)
        {
            var article = GetOneArticle(id);

            _context.Articles.Remove(article);

            _context.SaveChanges();
        }
    }
}
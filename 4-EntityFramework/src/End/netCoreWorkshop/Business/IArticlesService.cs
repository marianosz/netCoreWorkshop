using netCoreWorkshop.Entities;
using System.Collections.Generic;

namespace netCoreWorkshop.Business
{
    public interface IArticlesService
    {
        List<Article> GetAllArticles();

        Article AddArticle(Article article);

        Article EditArticle(int id, Article article);

        void DeleteArticle(int id);

        Article GetOneArticle(int id);
    }
}
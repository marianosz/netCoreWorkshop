using System.Collections.Generic;
using netCoreWorkshop.Entities;

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

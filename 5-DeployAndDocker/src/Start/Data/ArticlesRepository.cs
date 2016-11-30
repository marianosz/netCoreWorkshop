using ConsoleApplication.Entities;

namespace ConsoleApplication.Data
{
    public class ArticlesRepository : BaseRepository<Article>, IArticlesRepository
    {
        public ArticlesRepository(ArticlesContext context)
            : base(context)
        { }
    }
}
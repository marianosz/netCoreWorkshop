using ConsoleApplication.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApplication.Data
{
    public class ArticlesContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public ArticlesContext(DbContextOptions<ArticlesContext> options) :
              base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./articles.db");
        }
    }
}
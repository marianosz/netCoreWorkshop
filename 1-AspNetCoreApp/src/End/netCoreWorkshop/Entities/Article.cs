using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace netCoreWorkshop.Entities
{
    public class Article : IEntityBase
    {
        public static List<Article> DataSource = new List<Article>(new[] {
          new Article() { Id = 1, Title = "Intro to ASP.NET Core" },
          new Article() { Id = 2, Title = "Docker Fundamentals" },
          new Article() { Id = 3, Title = "Deploying to Azure with Docker" },
        });

        public int Id { get; set; }

        [Required]
        [DisplayName("Titulo")]
        public string Title { get; set; }
    }
}
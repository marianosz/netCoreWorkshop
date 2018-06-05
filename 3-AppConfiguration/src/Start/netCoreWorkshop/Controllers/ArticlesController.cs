using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using netCoreWorkshop.Entities;

namespace netCoreWorkshop.Controllers
{
    public class ArticlesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(Article.DataSource);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                article.Id = Article.DataSource.Count() + 1;
                Article.DataSource.Add(article);
                return RedirectToAction("Index");
            }

            return View(article);
        }
    }
}
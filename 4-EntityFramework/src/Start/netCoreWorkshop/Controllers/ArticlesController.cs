using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using netCoreWorkshop.Entities;
using netCoreWorkshop.Business;

namespace netCoreWorkshop.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(articlesService.GetAllArticles());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var article = articlesService.GetOneArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
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
                articlesService.AddArticle(article);

                return RedirectToAction("Index");
            }

            return View(article);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = articlesService.GetOneArticle(id);

            if (article == null)
            {
                return NotFound();
            }
            
            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(int id, Article article)
        {
            if (ModelState.IsValid)
            {
                var updatedArticle = articlesService.EditArticle(id, article);

                if (updatedArticle == null)
                {
                    return NotFound();
                }
             
                return RedirectToAction("Index");
            }

            return View(article);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {   
            var article = articlesService.GetOneArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            articlesService.DeleteArticle(id);

            return RedirectToAction("Index");
        }
    }
}


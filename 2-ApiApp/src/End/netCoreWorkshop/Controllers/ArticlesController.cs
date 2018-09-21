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
        public IActionResult Details(int id)
        {
            var article = Article.DataSource.Where(m => m.Id == id).FirstOrDefault();

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
                article.Id = Article.DataSource.Count() + 1;
                Article.DataSource.Add(article);
                return RedirectToAction("Index");
            }

            return View(article);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = Article.DataSource.Where(m => m.Id == id).FirstOrDefault();

            if (article == null)
            {
                return NotFound();
            }
            
            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(int id, Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var currentArticle = Article.DataSource.Where(m => m.Id == id).FirstOrDefault();

                if (currentArticle == null)
                {
                    return NotFound();
                }
                currentArticle.Title = article.Title;
             
                return RedirectToAction("Index");
            }

            return View(article);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = Article.DataSource.Where(m => m.Id == id).FirstOrDefault();

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var article = Article.DataSource.Where(m => m.Id == id).FirstOrDefault();

            if (article == null)
            {
                return NotFound();
            }

            Article.DataSource.Remove(article);

            return RedirectToAction("Index");
        }
    }
}


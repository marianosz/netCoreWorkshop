using ConsoleApplication.Data;
using ConsoleApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApplication.Articles
{
    [Route("/api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesRepository _articlesRepository;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IArticlesRepository articlesRepository, ILogger<ArticlesController> logger)
        {
            _articlesRepository = articlesRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Article>> Get() => await _articlesRepository.GetAllAsync();

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _articlesRepository.GetSingleAsync(id);
            if (article == null)
            {
                return NotFound(); // This makes it return 404; otherwise it will return a 204 (no content)
            }

            return new ObjectResult(article);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Article article)
        {
            _logger.LogDebug("Starting save");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _articlesRepository.Add(new Article { Title = article.Title });
            await _articlesRepository.SaveChangesAsync();

            _logger.LogDebug("Finished save");

            return CreatedAtAction(nameof(Get), new { id = article.Title }, article);
        }
    }
}
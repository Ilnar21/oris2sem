using HotMagazine.Domain.Models;
using HotMagazine.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HotMagazine.WebUI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace HotMagazine.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsPortalDbContext _context;

        public HomeController(NewsPortalDbContext context)
        {
            _context = context;
        }

        public IActionResult NewsDetails(int id)
        {
            var news = _context.News
                .Where(n => n.Id == id)
                .Select(n => new
                {
                    News = n,
                    Comments = n.Comments.OrderByDescending(c => c.PostedAt).ToList()
                })
                .FirstOrDefault();

            if (news == null)
                return NotFound();

            ViewBag.Comments = news.Comments;

            // Добавляем популярные новости (топ 5 по комментариям)
            ViewBag.PopularNews = _context.News
                .OrderByDescending(n => n.Comments.Count)
                .Take(5)
                .ToList();

            return View("SinglePost", news.News);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(int newsId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return RedirectToAction("NewsDetails", new { id = newsId });

            var comment = new Comment
            {
                AuthorName = User.Identity.Name,
                Content = content,
                PostedAt = DateTime.UtcNow,
                NewsId = newsId
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("NewsDetails", new { id = newsId });
        }


        public IActionResult Index()
        {
            var allNews = _context.News
                .OrderByDescending(n => n.PublishedAt)
                .ToList();

            var topStories = allNews.Take(5).ToList();

            var trendingNow = allNews
                .Where(n => !topStories.Select(ts => ts.Id).Contains(n.Id))
                .Take(3)
                .ToList();

            var shownIds = topStories.Select(ts => ts.Id)
                .Concat(trendingNow.Select(tn => tn.Id))
                .ToList();

            var latestArticles = allNews
                .Where(n => !shownIds.Contains(n.Id))
                .Take(4)
                .ToList();

            // Популярные новости по количеству комментариев (топ 5)
            var popularNews = _context.News
                .OrderByDescending(n => n.Comments.Count)
                .Take(5)
                .ToList();

            var viewModel = new HomeViewModel
            {
                TopStories = topStories,
                TrendingNow = trendingNow,
                LatestArticles = latestArticles,
                PopularNews = popularNews
            };

            return View(viewModel);
        }




        [Route("home/sportcategory/{category?}/{page?}")]
        public IActionResult SportCategory(string category = "All", int page = 1)
        {
            int pageSize = 5;

            var query = category.ToLower() == "all"
                ? _context.News.OrderByDescending(n => n.PublishedAt).AsQueryable()
                : _context.News.Where(n => n.Category.ToLower() == category.ToLower())
                    .OrderByDescending(n => n.PublishedAt)
                    .AsQueryable();

            int totalItems = query.Count();

            var news = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentCategory = category;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)System.Math.Ceiling((double)totalItems / pageSize);

            // Добавляем популярные новости (топ 5 по количеству комментариев)
            ViewBag.PopularNews = _context.News
                .OrderByDescending(n => n.Comments.Count)
                .Take(5)
                .ToList();

            return View("SportCategory", news);
        }


        [Route("home/categorynews/{category}")]
        public IActionResult CategoryNews(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return RedirectToAction("Index");

            var news = _context.News
                .Where(n => n.Category.ToLower() == category.ToLower())
                .OrderByDescending(n => n.PublishedAt)
                .ToList();

            ViewBag.CurrentCategory = category;

            // Добавляем популярные новости (топ 5 по количеству комментариев)
            ViewBag.PopularNews = _context.News
                .OrderByDescending(n => n.Comments.Count)
                .Take(5)
                .ToList();

            return View("SportCategory", news);
        }

        
        [Authorize]
        [HttpGet]
        public IActionResult CreateNews()
        {
            var model = new CreateNewsViewModel(); // категории уже заполняются внутри
            return View(model);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNews(CreateNewsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var authorName = User.Identity?.Name ?? "Anonymous"; 

            var news = new News
            {
                Title = model.Title,
                Category = model.Category,
                ImagePath = model.ImagePath,
                Content = model.Content,
                PublishedAt = DateTime.UtcNow,
                Author = authorName  
            };

            _context.News.Add(news);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        public IActionResult Error()
        {
            return View(); // Views/Shared/Error.cshtml
        }

        [Route("Home/StatusCode")]
        public IActionResult StatusCodeHandler(int code)
        {
            switch (code)
            {
                case 404:
                    return View("Error404");
                case 403:
                    return View("Error403");
                default:
                    return View("Error");
            }
        }

    }
}

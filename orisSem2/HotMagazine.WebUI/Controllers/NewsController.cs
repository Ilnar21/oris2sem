using HotMagazine.Domain.Models;
using HotMagazine.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HotMagazine.WebUI.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsPortalDbContext _context;

        public NewsController(NewsPortalDbContext context)
        {
            _context = context;
        }

        // Метод для вывода новостей по категории и странице
        // page - номер страницы, по умолчанию 1
        public IActionResult Category(string category, int page = 1)
        {
            int pageSize = 5; // Количество новостей на странице

            var query = category == null || category.ToLower() == "all"
                ? _context.News.OrderByDescending(n => n.PublishedAt).AsQueryable()
                : _context.News.Where(n => n.Category.ToLower() == category.ToLower())
                    .OrderByDescending(n => n.PublishedAt)
                    .AsQueryable();

            var totalItems = query.Count();

            var news = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentCategory = category ?? "All";
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)System.Math.Ceiling((double)totalItems / pageSize);

            return View("SportCategory", news);
        }
    }
}
using HotMagazine.Infrastructure.Data;
using HotMagazine.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotMagazine.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly NewsPortalDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminController(
            NewsPortalDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> AdminPanel(string authorId = null)
        {
            var newsQuery = _context.News.AsQueryable();
            
            if (!string.IsNullOrEmpty(authorId))
            {
                newsQuery = newsQuery.Where(n => n.Author == authorId);
            }
            var news = await newsQuery.ToListAsync();
            var users = await _userManager.Users.ToListAsync();

            var model = new AdminDashboardViewModel
            {
                NewsList = news,
                Users = users,
                SelectedAuthorId = authorId 
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("AdminPanel");

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                if (isAdmin)
                {
                    TempData["ErrorMessage"] = "You cannot delete a user with the 'Admin' role";
                    return RedirectToAction("AdminPanel");
                }

                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("AdminPanel");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

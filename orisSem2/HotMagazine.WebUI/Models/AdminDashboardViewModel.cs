using HotMagazine.Domain.Models;
using HotMagazine.Infrastructure.Identity;

public class AdminDashboardViewModel
{
    public List<News> NewsList { get; set; }
    public List<AppUser> Users { get; set; }
}
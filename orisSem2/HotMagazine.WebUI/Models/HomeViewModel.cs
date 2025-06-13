using HotMagazine.Domain.Models;
using System.Collections.Generic;

namespace HotMagazine.WebUI.Models
{
    public class HomeViewModel
    {
        public List<News> TopStories { get; set; }
        public List<News> TrendingNow { get; set; }
        
        public List<News> LatestArticles { get; set; }
        
        public List<News> PopularNews { get; set; }
    }
}
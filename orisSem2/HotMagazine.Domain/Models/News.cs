using HotMagazine.Domain.Models;

namespace HotMagazine.Domain.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string ImagePath { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }

        // Новое поле
        public string Author { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        
    }

}
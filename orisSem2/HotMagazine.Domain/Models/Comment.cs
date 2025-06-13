using System;
using System.ComponentModel.DataAnnotations;

namespace HotMagazine.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PostedAt { get; set; }

        public int NewsId { get; set; }  
        public News News { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HotMagazine.WebUI.Models
{
    public class CreateNewsViewModel
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }

        public List<SelectListItem> Categories { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Football", Text = "Football" },
            new SelectListItem { Value = "NBA", Text = "NBA" },
            new SelectListItem { Value = "Tennis", Text = "Tennis" }
        };
    }
}
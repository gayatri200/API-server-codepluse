﻿using CodePulse.API.Models.DTO;

namespace CodePulse1.Models.DTO
{
    public class BlogPostDTO
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }
        public bool IsVisible { get; set; }

        public List<CategoryDTO> Categories { get; set; }=new List<CategoryDTO>();
    }
}

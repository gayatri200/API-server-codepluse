using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse1.Models.DTO;
using CodePulse1.Repository.Implementation;
using CodePulse1.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse1.Controllers
{
    //https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class BlogpostController: ControllerBase
    {

            private readonly IblogpostRepository blogpostRepository;
        private readonly IcategoryRepository categoryrepository;

        public BlogpostController(IblogpostRepository blogpostRepository,IcategoryRepository categoryrepository)
            {
                this.blogpostRepository = blogpostRepository;
                this.categoryrepository = categoryrepository;
        }
            //create crud operation
            [HttpPost]
            public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDTO request)
            {
                //map dto to domain model
                var blogpost = new BlogPost
                {
                    
                    Title = request.Title,
                    ShortDescription = request.ShortDescription,
                    Content = request.Content,
                    FeaturedImageUrl = request.FeaturedImageUrl,

                    UrlHandle = request.UrlHandle,

                    PublishedDate = request.PublishedDate,

                    Author = request.Author,
                    IsVisible = request.IsVisible,

                    categories = new List<Category>()
                   
                };
            foreach(var categoryInt in request.Categories)
            {
                var existingcategory=await categoryrepository.GetById(categoryInt);
                if (existingcategory != null)
                {
                    blogpost.categories.Add(existingcategory);
                }
            }

              blogpost=  await blogpostRepository.CreateAsync(blogpost);

            //Convert Domain model to DTO

            var response = new BlogPostDTO
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                Content = blogpost.Content,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,

                UrlHandle = blogpost.UrlHandle,

                PublishedDate = blogpost.PublishedDate,

                Author = blogpost.Author,
                IsVisible = blogpost.IsVisible,

                Categories = blogpost.categories.Select(x=>new CategoryDTO
                {
                    Id=x.Id,
                    Name=x.Name,
                    UrlHandle=x.UrlHandle
                }).ToList()
            };




            return Ok(response);

                //Domain model to DTO


            }

        [HttpGet]

        public async Task<IActionResult> GetAllBlogPost()
        {
            var blogpost = await blogpostRepository.GetallAsync();

            //map domain model to dto
            var response =new List<BlogPostDTO>();

            foreach(var blogposts  in blogpost)
            {  
                response.Add(new BlogPostDTO
                { 
                    Id=blogposts.Id,
                    Author = blogposts.Author,
                    Title = blogposts.Title,
                    ShortDescription = blogposts.ShortDescription, 
                    Content = blogposts.Content,
                    FeaturedImageUrl= blogposts.FeaturedImageUrl,
                    PublishedDate=blogposts.PublishedDate,
                    IsVisible=blogposts.IsVisible,

                    Categories = blogposts.categories.Select(x => new CategoryDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });       
            }
            return Ok(response);
        }


    

    }

}

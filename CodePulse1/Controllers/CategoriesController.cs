using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse1.Models.DTO;
using CodePulse1.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    //https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IcategoryRepository categoryRepository;

        public CategoriesController(IcategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        //create crud operation
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO request)
        {
            //map dto to domain model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            await categoryRepository.CreateAsync(category);

            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);

            //Domain model to DTO


        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetallAsync();
            //map domain model to dto
            var response = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDTO
                { Id = category.Id, Name = category.Name, UrlHandle = category.UrlHandle }
              );
            }
            return Ok(response);
        }

        //https://localhost:7144/api/Categories/10
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var existingCategory = await categoryRepository.GetById(id);
            if (existingCategory is null)
            {
                return NotFound();
            }
            var response = new CategoryDTO
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle

            };
            return Ok(response);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditCategory([FromRoute] int id, UpdateCategoryRequest request)
        {
            //convert DTO to Domain model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle

            };
            category = await categoryRepository.UpdateAsync(category);
            if (category == null)
            {
                return NotFound();

            }
            //Convert Domain model to DTO
            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
           
            
                var category = await categoryRepository.DeleteCategory(id);

                if (category == null)
                {
                    return NotFound();
                }

            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
           
        }

      

    }
}

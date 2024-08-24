using Codepulse.Data;
using Codepulse.Models.Domain;
using Codepulse.Models.DTO;
using Codepulse.Repositories.Implementations;
using Codepulse.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Codepulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
         private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository=categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto requesdt)
        {
            //Map DTO to Domain Model

            var category = new Category
            {
                 Name = requesdt.Name,
                 UrlHandle = requesdt.UrlHandle
            };

            await _categoryRepository.CreateAsync(category);


               //domain model to dto

               var response = new CategoryDto
            {
                Id= category.Id, Name = category.Name, UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
          var categories = await  _categoryRepository.GetAllAsync();

        // Map domain to DTOS

            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
                    
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
          var existingCategory =  await _categoryRepository.GetByIdAsync(id);

            if(existingCategory is null)
            {
                return NotFound();
            }

            //domain model top dto

            var response = new CategoryDto
            {
                Id= existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryDto request)
        {
            //convert dto to domain modal

            var catetegory = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

             catetegory = await _categoryRepository.UpdateAsync(catetegory);

            if(catetegory is null)
            {
                return NotFound();
            }

            //convert domain to Dto

            var response = new Category
            {
                Id= catetegory.Id,
                Name = catetegory.Name,
                UrlHandle= catetegory.UrlHandle
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category =  await _categoryRepository.DeleteAsync(id);
            if(category is null)
            {
                return NotFound();
            }

            //convert to domain to DTO

            var response = new CategoryDto { 
                Id= category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }
    }
}

using ECommerceShop.DataAccess.Implementation;
using ECommerceShop.Entities.Models.Domain;
using ECommerceShop.Entities.Models.DTO;
using ECommerceShop.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceShop.Controllers
{
    [Route("[controller]")] // /api
    [ApiController]
    public class CategoryController:ControllerBase
    {
        private IUnitofWork _unitofWork;
        public CategoryController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;

        }

        [HttpPost]
        public  IActionResult Create([FromBody] CategoryCreateDto categoryDto) //Category category
        {
            /*if (category == null)
            {
                return BadRequest("Category data is required.");
            }
            category.CreatedDate = DateTime.Now;
            _unitofWork.Category.Add(category);
            _unitofWork.Complete();
            return Ok(category.Id);*/
            if (categoryDto == null)
            {
                return BadRequest("Category data is required.");
            }

            try
            {
                var category = new Category
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description,
                    CreatedDate = categoryDto.CreatedDate
                };

                _unitofWork.Category.Add(category);
                _unitofWork.Complete();

                return Ok(categoryDto.Name);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    

        
        [HttpGet]

        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetCategory()
        {
            
            var categories = await _unitofWork.Category.GetAllAsync();

            var categoryDtos = categories.Select(category => new CategoryReadDto
            {
                //Id = category.Id,
                Name = category.Name,
                Description = category.Description
            }).ToList();

            return Ok(categoryDtos);

        }

        [HttpDelete]
        public IActionResult DeleteCategory(CategoryDeleteDto categoryDeleteDto)
        {

            var records =  _unitofWork.Category.GetFirstOrDefault(x=>x.Id == categoryDeleteDto.Id);
            if(records == null)
            {
                NotFound();
            }
            _unitofWork.Category.Remove(records);
            _unitofWork.Complete();

            return Ok(records);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryUpdateDto category)
        {

            var existingCategory = _unitofWork.Category.GetFirstOrDefault(x => x.Id == id);
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.CreatedDate = category.CreatedDate;

            
            _unitofWork.Category.Update(existingCategory);
            _unitofWork.Complete();

            return Ok();

        }



    }
}

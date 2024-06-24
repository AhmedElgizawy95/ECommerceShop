using ECommerceShop.Entities.Models.Domain;
using ECommerceShop.Entities.Models.DTO;
using ECommerceShop.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceShop.Controllers
{

    [Route("[controller]")] // /api
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IUnitofWork _unitofWork;
        public ProductController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;

        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductCreateDto productDto) //Category category
        {
        
            if (productDto == null)
            {
                return BadRequest("Category data is required.");
            }
  
            try
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    CreatedDate = productDto.CreatedDate,
                    Price = productDto.Price,
                    Stock = productDto.Stock,
                    CategoryId= productDto.CategoryId,
                    ImageUrl= productDto.ImageUrl,

                };

                _unitofWork.Product.Add(product);
                _unitofWork.Complete();

                return Ok(productDto.Name);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProduct()
        {

            var products = await _unitofWork.Product.GetAllAsync();

            var productDtos = products.Select(product => new ProductReadDto
            {
                //Id = category.Id,
                Name = product.Name,
                Description = product.Description,
                CreatedDate = product.CreatedDate,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId= product.CategoryId,
                ImageUrl= product.ImageUrl,

            }).ToList();


            return Ok(productDtos);

        }

        [HttpDelete]
        public IActionResult DeleteProduct(ProductDeleteDto productDeleteDto)
        {

            var records = _unitofWork.Product.GetFirstOrDefault(x => x.ProductId == productDeleteDto.Id);
            if (records == null)
            {
                NotFound();
            }
            _unitofWork.Product.Remove(records);
            _unitofWork.Complete();

            return Ok(records);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductUpdateDto product)
        { 

            var existingProduct = _unitofWork.Product.GetFirstOrDefault(x => x.ProductId == id);
            if(product.Name != null)
            {
                existingProduct.Name = product.Name;
            }
            else
            {
                existingProduct.Name = existingProduct.Name;
            }

            if (product.Description != "string")
            {
                existingProduct.Description = product.Description;
            }
            else
            {
                existingProduct.Description = existingProduct.Description;
            }
            //existingProduct.Description = product.Description;


            if (product.Price != 0)
            {
                existingProduct.Price = product.Price;
            }
            else
            {
                existingProduct.Price = existingProduct.Price;
            }
            existingProduct.CreatedDate = product.CreatedDate;
            // existingProduct.Price = product.Price;
            if (product.Stock != 0)
            {
                existingProduct.Stock = product.Stock;
            }
            else
            {
                existingProduct.Stock = existingProduct.Stock;
            }
            //existingProduct.Stock = product.Stock;

            if (product.CategoryId != 0)
            {
                existingProduct.CategoryId = product.CategoryId;
            }
            else
            {
                existingProduct.CategoryId = existingProduct.CategoryId;
            }
            //existingProduct.CategoryId = product.CategoryId;

            if (product.ImageUrl != "string")
            {
                existingProduct.ImageUrl = product.ImageUrl;
            }
            else
            {
                existingProduct.ImageUrl = existingProduct.ImageUrl;
            }
            //existingProduct.ImageUrl = product.ImageUrl;


            _unitofWork.Product.Update(existingProduct);
            _unitofWork.Complete();

            return Ok();

        }



    }
}

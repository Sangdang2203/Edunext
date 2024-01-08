using Edunext_API.Helpers;
using Edunext_API.Models;
using Edunext_Model.DTOs.Product;
using Edunext_Model.Mapper;
using Edunext_Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Edunext_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public ProductsController(DatabaseContext _databaseContext) 
        {
            databaseContext = _databaseContext;
        }
        [HttpGet]
        public async Task<IEnumerable<ProductGet>> GetProducts()
        {
            var products = await databaseContext.Products.Include(product => product.Category).ToListAsync();
            var poductGets = Mapping.Mapper.Map<List<ProductGet>>(products);

            return poductGets;
        }


        [HttpGet("edit/{id}")]
        public async Task<ActionResult<ProductPost>> GetProductPost(int id)
        {
            var product = await databaseContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productPost = Mapping.Mapper.Map<ProductPost>(product);

            return productPost;
        }

        [HttpPost]
        public async Task<ActionResult<ProductPost>> Create([FromForm] ProductPost productPost)
        {
            try
            {

                var existedCode = await databaseContext.Products.AnyAsync(p => p.Code == productPost.Code);
                var existedName = await databaseContext.Products.AnyAsync(p => p.Name == productPost.Name);

                if (!existedCode && !existedName)
                {
                    Product product = Mapping.Mapper.Map<Product>(productPost);

                    product.ImageUrl = UploadFiles.SaveFile("ProductImage", productPost.Image);

                    await databaseContext.Products.AddAsync(product);
                    await databaseContext.SaveChangesAsync();

                    return CreatedAtAction("GetProductPost", new { id = product.Id }, product);
                }

                return BadRequest();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await databaseContext.Products.FindAsync(id);
            if (product != null)
            {
                if (product.ImageUrl != null)
                {
                    UploadFiles.DeleteFile(product.ImageUrl);
                }
                databaseContext.Products.Remove(product);
                await databaseContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductPut>> Update([FromForm] ProductPut product, int id)
        {
            var existedProduct = await databaseContext.Products.FindAsync(id);
            if(existedProduct != null)
            {
                try
                {
                    if (existedProduct.Image != null)
                    {
                        if (existedProduct.Image != null)
                        {
                            UploadFiles.DeleteFile(existedProduct.ImageUrl);
                        }
                        product.ImageUrl = UploadFiles.SaveFile("ProductImage", product.Image);
                    }
                    existedProduct.Code = product.Code;
                    existedProduct.Name = product.Name;
                    existedProduct.Price = (decimal)product.Price;
                    existedProduct.Quantity = (int)product.Quantity;
                    existedProduct.Description = product.Description;

                    databaseContext.Entry(existedProduct).CurrentValues.SetValues(product);
                    await databaseContext.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                    return BadRequest(ModelState);
                }
            }          
            return NotFound();
        }

    }
}

using Edunext_API.Helpers;
using Edunext_API.Models;
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
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await databaseContext.Products.ToListAsync();
            return products;
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProduct (int id)
        {
            var product = await databaseContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromForm] Product product, IFormFile file)
        {
            try
            {
                var existedCode = await databaseContext.Products.AnyAsync(p => p.Code == product.Code);
                var existedName = await databaseContext.Products.AnyAsync(p => p.Name == product.Name);

                if (existedCode)
                {
                    ModelState.AddModelError("Code", "This code already exists!");
                    return BadRequest(ModelState);
                }

                if (existedName)
                {
                    ModelState.AddModelError("Name", "This product already exists!");
                    return BadRequest(ModelState);
                }

                product.Image = UploadFiles.SaveFile("ProductImage", file);

                await databaseContext.Products.AddAsync(product);
                await databaseContext.SaveChangesAsync();

                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await databaseContext.Products.FindAsync(id);
            if (product != null)
            {
                if (product.Image != null)
                {
                    UploadFiles.DeleteFile(product.Image);
                }
                databaseContext.Products.Remove(product);
                await databaseContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Update([FromForm] Product product, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updatedProduct = await databaseContext.Products.FindAsync(product.Id);
            if(updatedProduct != null)
            {
                try
                {
                    if(file != null)
                    {
                        if(updatedProduct.Image != null)
                        {
                            UploadFiles.DeleteFile(updatedProduct.Image);
                        }
                        product.Image = UploadFiles.SaveFile("ProductImage", file);
                    }
                    updatedProduct.Code = product.Code;
                    updatedProduct.Name = product.Name;
                    updatedProduct.Price = product.Price;
                    updatedProduct.Quantity = product.Quantity;
                    updatedProduct.Description = product.Description;
                    databaseContext.Products.Update(updatedProduct);
                    await databaseContext.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return NotFound();
        }

    }
}

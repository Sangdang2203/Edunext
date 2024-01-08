using Edunext_API.Helpers;
using Edunext_API.Models;
using Edunext_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public CategoriesController(DatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await databaseContext.Categories.ToListAsync();
            return categories;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await databaseContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            return category;
        }

        
        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            try
            {
                var existedName = await databaseContext.Categories.AnyAsync(c => c.Name == category.Name);

                if (existedName)
                {
                    ModelState.AddModelError("Code", "This category already exists!");
                    return BadRequest(ModelState);
                }

                await databaseContext.Categories.AddAsync(category);
                await databaseContext.SaveChangesAsync();

                return CreatedAtAction("GetCategory", new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Edit(int id, Category category)
        {

            try
            {
                var existedCategory = await databaseContext.Categories.FindAsync(id);
                var existedName = await databaseContext.Categories.AnyAsync(p => p.Name == category.Name);
                if (existedCategory != null)
                {
                    if(existedName)
                    {
                        ModelState.AddModelError("Code", "This category already exists!");
                    }
                    databaseContext.Entry(existedCategory).CurrentValues.SetValues(category);
                    await databaseContext.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

    }
}

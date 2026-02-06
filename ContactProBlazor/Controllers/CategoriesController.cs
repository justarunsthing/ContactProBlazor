using ContactProBlazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ContactProBlazor.Client.Models;
using ContactProBlazor.Client.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ContactProBlazor.Controllers
{
    // api/categories
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController(ICategoryDTOService categoryDTOService, UserManager<ApplicationUser> userManager) : ControllerBase
    {
        // Authorize means userId will never be null
        // Is an expression bodied memner as it needs to be evaluated everytime this controller is accessed
        private string _userId => userManager.GetUserId(User)!;

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryAsync(int id)
        {
            try
            {
                CategoryDTO? category = await categoryDTOService.GetCategoryByIdAsync(id, _userId);

                return category == null ? NotFound() : category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategoriesAsync()
        {
            try
            {
                return await categoryDTOService.GetCategoriesAsync(_userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategoryAsync(CategoryDTO categoryDTO)
        {
            try
            {
                CategoryDTO createdCategory = await categoryDTOService.CreateCategoryAsync(categoryDTO, _userId);

                // Creates url to the GetCategory endpoint
                return CreatedAtAction(nameof(GetCategoryAsync), new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }
    }
}
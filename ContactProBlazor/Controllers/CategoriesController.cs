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
        // Is an expression as it needs to be evaluated
        private string _userId => userManager.GetUserId(User)!;

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
    }
}
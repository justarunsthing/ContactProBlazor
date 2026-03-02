using ContactProBlazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ContactProBlazor.Client.Models;
using Microsoft.AspNetCore.Authorization;
using ContactProBlazor.Client.Interfaces;

namespace ContactProBlazor.Controllers
{
    // api/contacts
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController(IContactDTOService contactDTOService, UserManager<ApplicationUser> userManager) : ControllerBase
    {
        // Authorize means userId will never be null
        // Is an expression bodied member as it needs to be evaluated everytime this controller is accessed
        private string _userId => userManager.GetUserId(User)!;

        [HttpGet]
        // [HttpGet("{id}")]
        public async Task<ActionResult<List<ContactDTO>>> GetContacts([FromQuery] int? categoryId)
        {
            try
            {
                if (categoryId != null)
                {
                    return await contactDTOService.GetContactsByCategoryAsync(categoryId.Value, _userId);
                }
                else
                {
                    return await contactDTOService.GetContactsAsync(_userId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }
    }
}
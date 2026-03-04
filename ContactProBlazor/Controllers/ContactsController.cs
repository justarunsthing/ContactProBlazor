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

        [HttpGet("id:{int}")]
        public async Task<ActionResult<ContactDTO>> GetContactById([FromRoute] int id)
        {
            try
            {
                ContactDTO? contact = await contactDTOService.GetContactByIdAsync(id, _userId);

                return contact == null ? NotFound() : contact;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        [HttpGet]
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

        [HttpGet("search")]
        public async Task<ActionResult<List<ContactDTO>>> SearchContacts([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest();
            }

            try
            {
                return await contactDTOService.SearchContactsAsync(searchTerm, _userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteContact([FromRoute] int id)
        {
            try
            {
                await contactDTOService.DeleteContactAsync(id, _userId);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }
    }
}
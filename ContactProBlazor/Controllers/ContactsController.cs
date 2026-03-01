using ContactProBlazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
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
    }
}
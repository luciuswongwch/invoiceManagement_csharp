using InvoiceManagement.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceManagement.Pages.Invoices
{
    public class DI_BasePageModel : PageModel
    {
        protected ApplicationDbContext Context { get; set; }
        protected IAuthorizationService AuthorizationService { get; set; }
        protected UserManager<IdentityUser> UserManager { get; set; }

        public DI_BasePageModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        {
            Context = context;
            AuthorizationService = authorizationService;
            UserManager = userManager;  
        }
    }
}

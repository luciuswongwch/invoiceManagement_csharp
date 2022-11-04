using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InvoiceManagement.Data;
using InvoiceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using InvoiceManagement.Authorization;

namespace InvoiceManagement.Pages.Invoices
{
    [AllowAnonymous]
    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Invoice> InvoiceList { get; set; }

        public async Task OnGetAsync()
        {
            if (Context.Invoices != null)
            {
                var invoices = from i in Context.Invoices 
                               select i;

                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                    User, new Invoice(), InvoiceOperation.Index);

                if (isAuthorized.Succeeded == false)
                    invoices = invoices.Where(i => i.InvoiceCreatorId == UserManager.GetUserId(User));

                InvoiceList = await invoices.ToListAsync();
            }
        }
    }
}

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
    public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || Context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await Context.Invoices.FirstOrDefaultAsync(m => m.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }
            else 
            {
                Invoice = invoice;
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Invoice, InvoiceOperation.Delete);

            if (isAuthorized.Succeeded == false)
                return Forbid();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || Context.Invoices == null)
            {
                return NotFound();
            }
            var invoice = await Context.Invoices.FindAsync(id);

            if (invoice != null)
            {
                Invoice = invoice;

                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Invoice, InvoiceOperation.Update);

                if (isAuthorized.Succeeded == false)
                    return Forbid();

                Context.Invoices.Remove(Invoice);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

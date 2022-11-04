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
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
            :base (context, authorizationService, userManager)
        {
        }

      public Invoice? Invoice { get; set; }

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
                User, Invoice, InvoiceOperation.Read);

            if (isAuthorized.Succeeded == false)
                return Forbid();

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int id, InvoiceStatus status)
        {
            Invoice = await Context.Invoices.FindAsync(id);
            if (Invoice == null)
                return NotFound();

            var invoiceOperation = status == InvoiceStatus.Approved
                ? InvoiceOperation.Approve
                : InvoiceOperation.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Invoice, invoiceOperation);

            if (isAuthorized.Succeeded == false)
                return Forbid();

            Invoice.InvoiceStatus = status;
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

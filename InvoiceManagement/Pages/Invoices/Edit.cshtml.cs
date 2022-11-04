using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceManagement.Data;
using InvoiceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using InvoiceManagement.Authorization;

namespace InvoiceManagement.Pages.Invoices
{
    public class EditModel : DI_BasePageModel
    {
        public EditModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
            : base (context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || Context.Invoices == null)
            {
                return NotFound();
            }

            var invoice =  await Context.Invoices.FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
            Invoice = invoice;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Invoice, InvoiceOperation.Update);

            if (isAuthorized.Succeeded == false)
                return Forbid();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Invoice.InvoiceCreatorId");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var invoice = await Context.Invoices.AsNoTracking().FirstOrDefaultAsync(m => m.Id == Invoice.Id);
            if (invoice == null)
                return Page();

            Invoice.InvoiceCreatorId = invoice.InvoiceCreatorId;
            if (Invoice.InvoiceAmount == invoice.InvoiceAmount &&
                Invoice.InvoiceMonth == invoice.InvoiceMonth &&
                Invoice.InvoiceAmount == invoice.InvoiceAmount)
                Invoice.InvoiceStatus = invoice.InvoiceStatus;
            else
                Invoice.InvoiceStatus = InvoiceStatus.Submitted;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Invoice, InvoiceOperation.Update);

            if (isAuthorized.Succeeded == false)
                return Forbid();

            Context.Attach(Invoice).State = EntityState.Modified;
            Context.SaveChanges();

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(Invoice.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InvoiceExists(int id)
        {
          return Context.Invoices.Any(e => e.Id == id);
        }
    }
}

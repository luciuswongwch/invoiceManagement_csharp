using InvoiceManagement.Authorization;
using InvoiceManagement.Data;
using InvoiceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace InvoiceManagement.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        public Dictionary<string, int> PendingInvoiceAmount { get; set; }
        public Dictionary<string, int> ApprovedInvoiceAmount { get; set; }
        public Dictionary<string, int> RejectedInvoiceAmount { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            var initializedDictionary = Enum.GetValues(typeof(InvoiceMonth))
               .Cast<InvoiceMonth>()
               .ToDictionary(t => t.ToString(), t => 0);

            PendingInvoiceAmount = new Dictionary<string, int>(initializedDictionary);
            ApprovedInvoiceAmount = new Dictionary<string, int>(initializedDictionary);
            RejectedInvoiceAmount = new Dictionary<string, int>(initializedDictionary);

            foreach (var invoice in _dbContext.Invoices.ToList())
            {
                switch(invoice.InvoiceStatus)
                {
                    case InvoiceStatus.Submitted:
                        PendingInvoiceAmount[invoice.InvoiceMonth.ToString()] += (int)invoice.InvoiceAmount;
                        break;
                    case InvoiceStatus.Approved:
                        ApprovedInvoiceAmount[invoice.InvoiceMonth.ToString()] += (int)invoice.InvoiceAmount;
                        break;
                    case InvoiceStatus.Rejected:
                        RejectedInvoiceAmount[invoice.InvoiceMonth.ToString()] += (int)invoice.InvoiceAmount;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Invoice amount")]
        [Range(0, int.MaxValue, ErrorMessage = "Invoice amount must be larger than or equal to 0")]
        public double InvoiceAmount { get; set; }

        [DisplayName("Invoice month")]
        public InvoiceMonth InvoiceMonth { get; set; }

        [DisplayName("Invoice receiver")]
        public string InvoiceReceiver { get; set; }

        public string InvoiceCreatorId { get; set; }

        [DisplayName("Invoice status")]
        public InvoiceStatus InvoiceStatus { get; set; }
    }

    public enum InvoiceMonth
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    public enum InvoiceStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}

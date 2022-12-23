using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Models.Entity
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new List<InvoiceDetails>();
        }

        public int InvoiceID { get; set; }


        [DisplayName("Invoice Number")]
        public int InvoiceNumber { get; set; }

        public bool IsProposal
        {
            get
            {
                return (this.InvoiceNumber == null || this.InvoiceNumber == 0);
            }
        }

        public Guid CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public string Name { get; set; }

        [DisplayName("Name/Notes")]
        [Required]
        public string Notes { get; set; }

        [DisplayName("Proposal Details")]
        public string ProposalDetails { get; set; }

        [DisplayName("Created")]
        public DateTime TimeStamp { get; set; }

        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        [DisplayName("Advance Payment Tax")]
        [Range(0.00, 100.0, ErrorMessage = "Value must be a % between 0 and 100")]
        public decimal AdvancePaymentTax { get; set; }

        public bool Paid { get; set; }

        
        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }

        #region Calculated fields
        public decimal VATAmount
        {
            get
            {
                return this.TotalWithVAT - this.NetTotal;
            }
        }

        /// <summary>
        /// Total before TAX
        /// </summary>
        public decimal NetTotal
        {
            get
            {
                if (InvoiceDetails == null)
                    return 0;

                return InvoiceDetails.Sum(i => i.Total);
            }
        }

        public decimal AdvancePaymentTaxAmount
        {
            get
            {
                if (InvoiceDetails == null)
                    return 0;

                return NetTotal * (AdvancePaymentTax / 100);
            }
        }

        /// <summary>
        /// Total with tax
        /// </summary>
        public decimal TotalWithVAT
        {
            get
            {
                if (InvoiceDetails == null)
                    return 0;

                return InvoiceDetails.Sum(i => i.TotalPlusVAT);
            }
        }

        /// <summary>
        /// Total with VAT minus advanced tax payment 
        /// </summary>
        public decimal TotalToPay
        {
            get
            {
                return TotalWithVAT - AdvancePaymentTaxAmount;
            }
        }

        public decimal TotalDiscountedPrice
        {
            get
            {
                return TotalWithVAT - AdvancePaymentTaxAmount;
            }
        }

        public InvoiceType InvoiceType { get; set; }


        #endregion
    }
}

public enum InvoiceType
{
    ProForma = 0,
    Invoice = 1,
}

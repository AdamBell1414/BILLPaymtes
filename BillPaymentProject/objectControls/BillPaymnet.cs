using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPaymentProject.objectControls
{
  public class BillPaymnet
    {

        public int? UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } // SuperAdmin, VendorAdmin, Custome
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }

       
        public int? VendorID { get; set; }
        public string VendorName { get; set; }
        public string ContactEmail { get; set; }
        public string VendorCode {  get; set; }
        public string ContactPhone { get; set; }
        public decimal? Balance { get; set; }
        public int? CreatedBy { get; set; }

       
        public int? UtilityID { get; set; }
        public string UtilityName { get; set; }
        public string UtilityCode { get; set; }
        public bool? UtilityIsActive { get; set; }
        public int? RegisteredBy { get; set; }

       
        public int? CustomerID { get; set; }
        public string ReferenceNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal? AccountBalance { get; set; }

        
        public Guid? TransactionID { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
        public bool? SentToMomo { get; set; }
        public string UtilityTransactionReference { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime? ProcessedAt { get; set; }

        
        public int? RequestID { get; set; }
        public string MomoStatus { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? ResponseTime { get; set; }
        public string RawRequest { get; set; }
        public string RawResponse { get; set; }

        
        public int? LogID { get; set; }
        public string Action { get; set; }
        public string Module { get; set; }
        
        public string IPAddress { get; set; }

       
        public string ResponseStatus { get; set; }
        public string LogFilePath { get; set; }

    }
}

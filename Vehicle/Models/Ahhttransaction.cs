using System;
using System.Collections.Generic;

#nullable disable

namespace Vehicle.Models
{
    public partial class Ahhttransaction
    {
        public int Id { get; set; }
        public string BuyerBankName { get; set; }
        public string BuyerAccountName { get; set; }
        public string BuyerAccountNumber { get; set; }
        public string BuyerMobileNumber { get; set; }
        public string SellerBankName { get; set; }
        public string SellerAccountName { get; set; }
        public string SellerAccountNumber { get; set; }
        public string SellerMobileNumber { get; set; }
        public string TransactionDescription { get; set; }
        public string AgreedPrice { get; set; }
        public string ShippingCost { get; set; }
        public string AhhtCommission { get; set; }
        public string TotalCost { get; set; }
        public string ConfirmationCode { get; set; }
        public bool? BuyerCodeConfirmed { get; set; }
        public bool? SellerCodeConfirmed { get; set; }
        public bool? Paid { get; set; }
        public string TransactionCode { get; set; }
        public string BuyerEmailAddress { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionStatus { get; set; }
        public string SellertTransactionCode { get; set; }
        public string Reasons { get; set; }
        public bool? Processed { get; set; }
        public bool? Deleted { get; set; }
        public bool? Completed { get; set; }
        public string ProcessedBy { get; set; }
        public bool? Viewed { get; set; }
        public string RefundDate { get; set; }
        public string PaymentDate { get; set; }
    }
}

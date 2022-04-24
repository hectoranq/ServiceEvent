using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Payments
    {
        public int Id { get; set; }
        public int? Accountid { get; set; }
        public string Description { get; set; }
        public string ReferenceCode { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TaxReturnBase { get; set; }
        public string Currency { get; set; }
        public string Signature { get; set; }
        public bool? Test { get; set; }
        public string BuyerEmail { get; set; }
        public string ResponseUrl { get; set; }
        public string ConfirmationUrl { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingCountry { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

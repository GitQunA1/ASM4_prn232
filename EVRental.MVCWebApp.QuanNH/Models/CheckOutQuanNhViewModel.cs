using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EVRental.MVCWebApp.QuanNH.Models
{
    public class CheckOutQuanNhViewModel
    {
        [DisplayName("ID")]
        public int CheckOutQuanNhid { get; set; }

        [DisplayName("Check-out Time")]
        [DataType(DataType.DateTime)]
        public DateTime? CheckOutTime { get; set; }

        [DisplayName("Return Date")]
        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }

        [DisplayName("Extra Cost")]
        [DataType(DataType.Currency)]
        public decimal? ExtraCost { get; set; }

        [DisplayName("Total Cost")]
        [DataType(DataType.Currency)]
        public decimal? TotalCost { get; set; }

        [DisplayName("Late Fee")]
        [DataType(DataType.Currency)]
        public decimal? LateFee { get; set; }

        [DisplayName("Paid")]
        public bool IsPaid { get; set; }

        [DisplayName("Damage Reported")]
        public bool IsDamageReported { get; set; }

        [DisplayName("Notes")]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }

        [DisplayName("Customer Feedback")]
        [DataType(DataType.MultilineText)]
        public string? CustomerFeedback { get; set; }

        [DisplayName("Payment Method")]
        public string? PaymentMethod { get; set; }

        [DisplayName("Staff Signature")]
        public string? StaffSignature { get; set; }

        [DisplayName("Customer Signature")]
        public string? CustomerSignature { get; set; }

        [DisplayName("Return Condition ID")]
        public int? ReturnConditionId { get; set; }

        [DisplayName("Return Condition")]
        public string? ReturnConditionName { get; set; }
    }
}

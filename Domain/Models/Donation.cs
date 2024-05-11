using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Donation : BaseEntity
    {
        public Donor Donor { get; set; }
        public int DonorId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public Ngo Ngo { get; set; }
        public int NgoId { get; set; }
        public string PaymentReference { get; set; }
        public string Status { get; set; }
    }

    public class DonationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Ngo : BaseEntity
    { 
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string? PhotoUrl { get; set; }
        public string Address { get; set; }
        public decimal FundsGenerated { get; set; }
    }

    public class NgoResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

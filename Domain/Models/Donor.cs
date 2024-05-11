using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Donor : BaseEntity
    {
        public string? LegalName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => string.Join(" ", new string[] { FirstName, MiddleName, LastName }.Where(a => !string.IsNullOrWhiteSpace(a)));
        public string EmailAddress { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Occupation { get; set; }
        public bool IsAnonymous { get; set; }
    }
}

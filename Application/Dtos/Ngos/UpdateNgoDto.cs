using Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Ngos
{
    public class UpdateNgoDto : BaseDto
    {
           [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(60)]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string? PhotoUrl { get; set; }
        public string Address { get; set; }
        public decimal FundsGenerated { get; set; }
    }
}

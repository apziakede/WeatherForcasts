using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Common
{
    public class BaseDto
    {
   
        public DateTime? LastModifiedOn { get; set; }

        public string? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}

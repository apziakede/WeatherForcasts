﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public string? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}

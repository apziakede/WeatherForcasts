﻿using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ngos.Queries
{
    public class GetNgoQuery : IRequest<Ngo>
    {
        [Required]
        public int Id { get; set; }
    }
}

﻿using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ngos.Queries
{
    public class GetAllNgoQuery : IRequest<IEnumerable<Ngo>>
    { 
    }
}

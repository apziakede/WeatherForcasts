using Application.Dtos.Ngos;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ngos.Commands
{
    public class UpdateNgoCommand : IRequest<NgoResponse>
    {
        public UpdateNgoDto UpdateNgo { get; set; }
    }
}

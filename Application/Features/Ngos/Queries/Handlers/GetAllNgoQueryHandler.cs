using Domain.Models;
using Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ngos.Queries.Handlers
{
    public class GetAllNgoQueryHandler : IRequestHandler<GetAllNgoQuery, IEnumerable<Ngo>>
    {
        private readonly AppDbContext _appDbContext;

        public GetAllNgoQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Ngo>> Handle(GetAllNgoQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Ngos.ToListAsync();
        }
    }
}

using Domain.Models;
using Infrastructure.DataContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ngos.Queries.Handlers
{
    public class GetNgoQueryHandler : IRequestHandler<GetNgoQuery, Ngo>
    {
        private readonly AppDbContext _context;

        public GetNgoQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ngo> Handle(GetNgoQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Ngos.FindAsync(request.Id);
            if (result != null) 
                return result;
            throw new NullReferenceException("No record was found");
        }
    }
}

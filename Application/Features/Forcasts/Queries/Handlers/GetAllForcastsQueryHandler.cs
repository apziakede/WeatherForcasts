using Domain.Models;
using Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Forcasts.Queries.Handlers
{
    public class GetAllForcastsQueryHandler : IRequestHandler<GetAllForcastsQuery, List<WeatherForecast>>
    {
        private readonly AppDbContext _dbContext;

        public GetAllForcastsQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WeatherForecast>> Handle(GetAllForcastsQuery request, CancellationToken cancellationToken)
        {
           var result= await _dbContext.Forcasts.ToListAsync();
            if (result != null) return result;
            throw new NullReferenceException("No record was found");
        }
    }
}

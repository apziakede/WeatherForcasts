using Domain.Models;
using Infrastructure.DataContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Forcasts.Commands.Handlers
{
    public class CreateForcastCommandHandler : IRequestHandler<CreateForcastCommand, WeatherForecast>
    {
        private readonly AppDbContext _dbContext;

        public CreateForcastCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<WeatherForecast> Handle(CreateForcastCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Forcasts.Add(request.NewForcast);
            await _dbContext.SaveChangesAsync();
            return request.NewForcast;
        }
    }
}

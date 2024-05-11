using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Forcasts.Commands
{
    public class CreateForcastCommand : IRequest<WeatherForecast>
    {
        public WeatherForecast NewForcast { get; set; }
    }
}

using Domain.Models;
using Infrastructure.DataContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ngos.Commands.Handlers
{
    public class CreateNgoCommandHandler : IRequestHandler<CreateNgoCommand, NgoResponse>
    {
        private readonly AppDbContext _dbContext;

        public CreateNgoCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<NgoResponse> Handle(CreateNgoCommand request, CancellationToken cancellationToken)
        {
            var requests = request.NewNgo;
            Ngo ngo = new()
            {
                Name = requests.Name,
                EmailAddress = requests.EmailAddress,
                PhoneNumber = requests.PhoneNumber,
                Country = requests.Country,
                PhotoUrl = requests.PhotoUrl,
                Address = requests.Address,
                FundsGenerated = requests.FundsGenerated,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _dbContext.Ngos.Add(ngo);
            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return new NgoResponse
                {
                    IsSuccess = true,
                    Message = "NGO details created successfully."
                };

            return new NgoResponse
            {
                IsSuccess = false,
                Message = "An error occurred while saving the details."
            };
        }
    }
}

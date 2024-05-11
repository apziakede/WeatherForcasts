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
    public class UpdateNgoCommandHandler : IRequestHandler<UpdateNgoCommand, NgoResponse>
    {
        private readonly AppDbContext _appDbContext;

        public UpdateNgoCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<NgoResponse> Handle(UpdateNgoCommand request, CancellationToken cancellationToken)
        {
            var _request = request.UpdateNgo;
            var ngoToUpate = _appDbContext.Ngos.FindAsync(_request.Id).Result;
            if (_request == null)
                return new NgoResponse
                {
                    IsSuccess = false,
                    Message = "The model is null."
                };

            if (ngoToUpate == null)
                return new NgoResponse
                {
                    IsSuccess = false,
                    Message = "The record you are trying to update does not exist."
                };

            ngoToUpate.Address = _request.Address;
            ngoToUpate.Country = _request.Country;
            ngoToUpate.EmailAddress = _request.EmailAddress;
            ngoToUpate.FundsGenerated = _request.FundsGenerated;
            ngoToUpate.IsDeleted = _request.IsDeleted;
            ngoToUpate.LastModifiedBy = _request.LastModifiedBy;
            ngoToUpate.LastModifiedOn = DateTime.UtcNow;
            ngoToUpate.Name = _request.Name;
            ngoToUpate.PhoneNumber = _request.PhoneNumber;
            ngoToUpate.PhotoUrl = _request.PhotoUrl; 

            _appDbContext.Ngos.Update(ngoToUpate);
            var result = await _appDbContext.SaveChangesAsync();
            if (result > 0)
                return new NgoResponse
                {
                    IsSuccess = true,
                    Message = "NGO details updated successfully."
                };
            return new NgoResponse
            {
                IsSuccess = false,
                Message = "An error occurred while trying to update the details."
            };
        }
    }
}

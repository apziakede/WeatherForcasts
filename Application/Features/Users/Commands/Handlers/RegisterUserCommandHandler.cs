using Infrastructure.DataContext;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserManagerResponse>
    { 
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
         
        public RegisterUserCommandHandler(UserManager<IdentityUser> userManager, IConfiguration configuration)
        { 
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserManagerResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var model=request.RegisterUserModel;
            if (model == null)
                throw new NullReferenceException("Register model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password does not match the password.",
                    IsSuccess = false
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (result.Succeeded)
            {
                //TODO: Send confirmation email
                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true
                };
            }
            return new UserManagerResponse
            {
                Message = "Could not create the user",
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description)
            };
        }
    }
}

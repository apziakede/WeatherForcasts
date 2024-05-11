 using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserManagerResponse>
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public LoginUserCommandHandler(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserManagerResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var model=request.LoginUserModel;
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (model == null)
                throw new NullReferenceException("Login model is null");

            if (model.Password.IsNullOrEmpty())
            {
                return new UserManagerResponse
                {
                    Message = "Password field cannot be empty.",
                    IsSuccess = false
                };
            }

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "User not found.",
                    IsSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Invalid Password provided.",
                    IsSuccess = false
                };

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenStr,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }
    }
}

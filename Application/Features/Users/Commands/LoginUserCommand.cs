using Application.Dtos.Users; 
using Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public class LoginUserCommand : IRequest<UserManagerResponse>
    {
        public LoginUserDto LoginUserModel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Result;
using MediatR;

namespace Application.Commands.Users
{
    public class CreateUserCommand : IRequest<Result<string>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
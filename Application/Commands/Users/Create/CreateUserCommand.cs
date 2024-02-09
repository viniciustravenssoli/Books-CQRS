using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Domain.Result;
using MediatR;

namespace Application.Commands.Users
{
    public class CreateUserCommand : IRequest<Result<string>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = PasswordErrors.PasswordDoNotMatch)]
        public string VerifyPassword { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Users;
using FluentValidation;

namespace Application.Validations.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().NotNull().MinimumLength(3);
            
            RuleFor(x => x.Email)
                .EmailAddress().NotEmpty().NotNull();
            
            RuleFor(x => x.Password)
                .NotEmpty().NotNull();
             
        }
    }
}
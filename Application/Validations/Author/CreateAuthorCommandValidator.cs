using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Authors.CreateAuthor;
using FluentValidation;

namespace Application.Validations.Author
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().MinimumLength(3);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Books;
using FluentValidation;

namespace Application.Validations.Book
{
    public class CrateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CrateBookCommandValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().NotNull();

        }
    }
}
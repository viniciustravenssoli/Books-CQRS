using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Result;
using MediatR;

namespace Application.Commands.Genres.Delete
{
    public class DeleteGenreCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Result;
using MediatR;

namespace Application.Commands.Genres
{
    public class CreateGenreCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
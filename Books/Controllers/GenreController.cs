using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Genres;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class GenreController : BaseController
    {
        public GenreController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
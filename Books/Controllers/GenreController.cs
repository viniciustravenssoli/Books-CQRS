using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Genres;
using Application.Commands.Genres.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class GenreController : BaseController
    {
        public GenreController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteGenreCommand { Id = id };
            var result = await _mediator.Send(command);

            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }
    }
}
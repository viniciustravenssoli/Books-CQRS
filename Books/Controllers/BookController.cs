using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class BookController : BaseController
    {
        public BookController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }
    }
}
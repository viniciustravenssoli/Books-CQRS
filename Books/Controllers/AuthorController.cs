using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Authors.CreateAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class AuthorController : BaseController
    {
        public AuthorController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands.Comments.PostComment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class CommnetController : BaseController
    {
        public CommnetController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentCommand command)
        {
            var userIdToken = User.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;

            command.UserId = userIdToken;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
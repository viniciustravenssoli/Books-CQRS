using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Authors.CreateAuthor;
using Application.Commands.Authors.DeleteAuthor;
using Application.Commands.Authors.UpdateAuthor;
using Application.Queries.Author;
using Application.Queries.Author.GetAuthorByIdQuery;
using Application.Queries.Author.GetAuthorsByName;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class AuthorController : BaseController
    {
        public AuthorController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] PageQuery pageQuery)
        {
            var query = new GetAllAuthorQuery(pageQuery);
            var result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetAuthorByIdQuery(id);
            var result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }

        [HttpGet("getByName")]
        public async Task<IActionResult> GetByName([FromQuery] PageQuery pageQuery, string name)
        {
            var query = new GetAuthorsByNameQuery(name, pageQuery);
            var result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateAuthorCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAuthorCommand { Id = id };
            var result = await _mediator.Send(command);

            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }
    }
}
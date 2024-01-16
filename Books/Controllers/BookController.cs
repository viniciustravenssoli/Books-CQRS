using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Books;
using Application.Commands.Books.Delete;
using Application.Queries.Author;
using Application.Queries.Book.GetAllBook;
using Application.Queries.Book.GetBookByAuthor;
using Application.Queries.Book.GetBookById;
using Domain.Helpers;
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

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] PageQuery pageQuery)
        {
            var query = new GetAllBookQuery(pageQuery);
            var result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetBookByIdQuery(id);
            var result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }

        [HttpGet("get-all-by-AuthorId/{authorId}")]
        public async Task<IActionResult> GetAllByAuthorId([FromQuery] PageQuery pageQuery, int authorId)
        {
            var query = new GetAllBookByAuthorQuery(pageQuery, authorId);
            var result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteBookCommand { Id = id };
            var result = await _mediator.Send(command);

            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }
    }
}
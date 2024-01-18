using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Result;
using MediatR;

namespace Application.Commands.Books
{
    public class CreateBookCommand : IRequest<Result<int>>
    {
        public CreateBookCommand(int bookId, string titulo, int authorId, int genreId)
        {
            BookId = bookId;
            Titulo = titulo;
            AuthorId = authorId;
            GenreId = genreId;
        }

        public int BookId { get; set; }
        public string Titulo { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Titulo { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }

        public static implicit operator BookViewModel(Book book)
        {
            if (book is null) 
                return null;
            return new()
            {
                BookId = book.BookId,
                Titulo = book.Titulo,
                Author = book.Author,
                Genre = book.Genre
            };
        }
    }
}
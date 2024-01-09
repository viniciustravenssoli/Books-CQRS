using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class Book : Base
    {
        public Book(int bookId, string titulo, int authorId, int genreId)
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


        public Author Author { get; set; }
        public Genre Genre { get; set; }

        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; }
    }
}

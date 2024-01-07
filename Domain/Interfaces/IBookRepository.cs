using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<int> CreateBookAsync(Book book);
        Task<List<Book>> GetAllAsync(int top, int skip);
        Task<List<Book>> GetAllByGenre(int top, int skip, string genre);
        Task<List<Book>> GetAllByAuthor(int top, int skip, string genre);
    }
}

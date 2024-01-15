using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CreateBookAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            return book.BookId;
        }

        public async Task<List<Book>> GetAllAsync(int top, int skip)
        {
            var books = await _dbContext.Books
                                .AsNoTracking()
                                .Skip(skip)
                                .Take(top)
                                .ToListAsync();

            return books;
        }

        public async Task<List<Book>> GetAllByAuthor(int top, int skip, string author)
        {
            var books = await _dbContext.Books
                .AsNoTracking()
                .Where(x => x.Author.Name.Equals(author, StringComparison.OrdinalIgnoreCase))
                .Skip(skip)
                .Take(top)
                .ToListAsync();

            return books;
        }

        public async Task<List<Book>> GetAllByGenre(int top, int skip, string genre)
        {
            var books = await _dbContext.Books
                .AsNoTracking()
                .Where(x => x.Genre.Name.Equals(genre, StringComparison.OrdinalIgnoreCase))
                .Skip(skip)
                .Take(top)
                .ToListAsync();

            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.BookId == id);

            return book;
        }
    }
}
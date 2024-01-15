using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CreateAuthorAsync(Author author)
        {
            await _dbContext.Authors.AddAsync(author);
            return author.Id;
        }

        public Task<List<Author>> GetAllAsync(int top, int skip)
        {
            throw new NotImplementedException();
        }

        public Task<List<Author>> GetAllByName(int top, int skip, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _dbContext.Authors
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
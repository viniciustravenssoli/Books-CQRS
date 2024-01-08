using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infra.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<int> CreateAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<List<Author>> GetAllAsync(int top, int skip)
        {
            throw new NotImplementedException();
        }

        public Task<List<Author>> GetAllByName(int top, int skip, string name)
        {
            throw new NotImplementedException();
        }
    }
}
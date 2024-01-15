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

        public async Task<List<Author>> GetAllAsync(int top, int skip)
        {
            return await _dbContext.Authors
                .Skip(skip)
                .Take(top)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Author>> GetAllByName(int top, int skip, string name)
        {
            var filteredAuthors = await _dbContext.Authors
                .Where(a => EF.Functions.Like(a.Name, $"%{name}%"))
                .Skip(skip)
                .Take(top)
                .ToListAsync();

            // only work with lowercase
            // var filteredAuthors = await _dbContext.Authors
            //     .Where(a => a.Name.Contains(name))
            //     .Skip(skip)
            //     .Take(top)
            //     .ToListAsync();
            
            return filteredAuthors;
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _dbContext.Authors
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
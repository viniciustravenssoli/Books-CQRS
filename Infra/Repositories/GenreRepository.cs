using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CreateGenreAsync(Genre genre)
        {
            await _dbContext.Genres.AddAsync(genre);
            return genre.Id;
        }

        public Task<List<Genre>> GetAllAsync(int top, int skip)
        {
            throw new NotImplementedException();
        }

        public Task<List<Genre>> GetAllByName(int top, int skip, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _dbContext.Genres
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
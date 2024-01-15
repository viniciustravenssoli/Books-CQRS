using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        Task<int> CreateGenreAsync(Genre genre);
        Task<Genre> GetGenreByIdAsync(int id);
        Task<List<Genre>> GetAllAsync(int top, int skip);
        Task<List<Genre>> GetAllByName(int top, int skip, string name);
    }
}

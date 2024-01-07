using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<int> CreateAuthorAsync(Author author);
        Task<List<Author>> GetAllAsync(int top, int skip);
        Task<List<Author>> GetAllByName(int top, int skip, string name);
    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<int> CreateCommentAsync(Comment comment);
        Task<List<Comment>> GetAllAsync(int top, int skip);
        Task<List<Comment>> GetAllByBook(int top, int skip, int bookId);
    }
}

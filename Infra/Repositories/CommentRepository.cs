using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CreateCommentAsync(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
            return comment.Id;
        }

        public async Task<List<Comment>> GetAllAsync(int top, int skip)
        {
            var allComments = await _dbContext.Comments 
                                    .AsNoTracking()
                                    .ToListAsync();
            
            return allComments;
        }

        public async Task<List<Comment>> GetAllByBook(int top, int skip, int bookId)
        {
            var commentsFromBook = await _dbContext.Comments
            .AsNoTracking()
            .Where(x => x.BookId == bookId)
            .Include(x => x.User)
            .OrderByDescending(x => x.CommentDate)
            .Skip(skip)
            .Take(top)
            .ToListAsync();

            return commentsFromBook;
        }
    }
}
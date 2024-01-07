using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenreRepository Genre { get; }
        ICommentRepository Comment { get; }
        IAuthorRepository Author { get; }
        IBookRepository Book { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

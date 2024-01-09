using Domain.Interfaces;
using Infra.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infra
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction _dbTransaction;
        private readonly IMediator _mediator;

        public UnitOfWork(
            AppDbContext dbContext,
            IBookRepository book,
            IGenreRepository genre,
            IAuthorRepository author,
            ICommentRepository comment,
            IMediator mediator)
        {
            _dbContext = dbContext;
            Book = book;
            Genre = genre;
            Author = author;
            Comment = comment;
            _mediator = mediator;
        }

        public IBookRepository Book { get; }
        public IGenreRepository Genre { get; }
        public IAuthorRepository Author { get; }
        public ICommentRepository Comment { get; }

        public async Task BeginTransactionAsync()
        {
            _dbTransaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbTransaction.CommitAsync();
            }
            catch (Exception)
            {
                await _dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(_dbContext);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}

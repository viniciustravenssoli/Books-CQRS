using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction _dbTransaction;
        private readonly IMediator _mediator;

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

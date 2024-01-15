using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Commands.Books.Delete
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Book.GetBookById(request.Id);

            if (book is null)
                return Result<string>.Failure(BookErrors.NotFoundBook, ResultStatusCodeEnum.NotFound);

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Book.DeleteAsync(book);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<string>.Success(book.BookId.ToString());
        }
    }
}
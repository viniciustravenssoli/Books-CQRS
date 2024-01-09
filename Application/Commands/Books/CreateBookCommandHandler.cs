using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Genres;
using Application.Errors;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Commands.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<int>>
    {
         private readonly IUnitOfWork _unitOfWork;

        public CreateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            Book book = new(request.BookId, request.Titulo, request.AuthorId, request.GenreId);

            var genre = await _unitOfWork.Genre.GetGenreByIdAsync(book.GenreId);
            if (genre is null)
                return Result<int>.Failure(BookErrors.NotFoundDonorGenre, ResultStatusCodeEnum.NotFound);

            var author = await _unitOfWork.Author.GetAuthorByIdAsync(book.AuthorId);
            if (author is null)
                return Result<int>.Failure(BookErrors.NotFoundDonorAuthor, ResultStatusCodeEnum.NotFound);

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Book.CreateBookAsync(book);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(book.BookId);
        }
    }
}
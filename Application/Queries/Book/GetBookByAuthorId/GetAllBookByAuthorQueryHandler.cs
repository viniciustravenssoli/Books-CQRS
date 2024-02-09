using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Application.Queries.Book.GetAllBook;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Queries.Book.GetBookByAuthor
{
    public class GetAllBookByAuthorQueryHandler : IRequestHandler<GetAllBookByAuthorQuery, Result<List<BookViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBookByAuthorQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BookViewModel>>> Handle(GetAllBookByAuthorQuery request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Author.GetAuthorByIdAsync(request.Id);

            if (author is null)
            {
                return Result<List<BookViewModel>>.Failure(AuthorErrors.NotFoundAuthor);
            }

            var bookList = await _unitOfWork.Book.GetAllByAuthorId(request.PageQuery.Top, request.PageQuery.Skip, request.Id);
            return Result<List<BookViewModel>>.Success(bookList.Select(b => (BookViewModel)b).ToList());
        }
    }
}
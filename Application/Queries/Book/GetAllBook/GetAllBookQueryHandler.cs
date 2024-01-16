using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Queries.Book.GetAllBook
{
    public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, Result<List<BookViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBookQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BookViewModel>>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {
            var bookList = await _unitOfWork.Book.GetAllAsync(request.PageQuery.Top, request.PageQuery.Skip);
            return Result<List<BookViewModel>>.Success(bookList.Select(b => (BookViewModel)b).ToList());
        }
    }
}
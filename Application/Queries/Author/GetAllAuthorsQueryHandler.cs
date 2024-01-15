using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Queries.Author
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorQuery, Result<List<AuthorViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<AuthorViewModel>>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
        {
            var donorList = await _unitOfWork.Author.GetAllAsync(request.PageQuery.Top, request.PageQuery.Skip);
            return Result<List<AuthorViewModel>>.Success(donorList.Select(a => (AuthorViewModel)a).ToList());
        }
    }
}
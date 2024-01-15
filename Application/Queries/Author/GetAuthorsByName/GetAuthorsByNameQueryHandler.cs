using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Queries.Author.GetAuthorsByName
{
    public class GetAuthorsByNameQueryHandler : IRequestHandler<GetAuthorsByNameQuery, Result<List<AuthorViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorsByNameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<AuthorViewModel>>> Handle(GetAuthorsByNameQuery request, CancellationToken cancellationToken)
        {
            var authorList = await _unitOfWork.Author.GetAllByName(request.PageQuery.Top, request.PageQuery.Skip, request.Name);
            return Result<List<AuthorViewModel>>.Success(authorList.Select(a => (AuthorViewModel)a).ToList());
        }
    }
}
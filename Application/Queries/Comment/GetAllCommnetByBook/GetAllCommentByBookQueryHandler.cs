using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Queries.Comment.GetAllCommnetByBook
{
    public class GetAllCommentByBookQueryHandler : IRequestHandler<GetAllCommentByBookQuery, Result<List<CommentViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCommentByBookQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<CommentViewModel>>> Handle(GetAllCommentByBookQuery request, CancellationToken cancellationToken)
        {
            var commentList = await _unitOfWork.Comment.GetAllByBook(request.PageQuery.Top, request.PageQuery.Skip, request.BookId);

            var commentViewModelList = commentList
                .Select(a => (CommentViewModel)a)
                .ToList();

            return Result<List<CommentViewModel>>.Success(commentViewModelList);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Commands.Comments.PostComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await _unitOfWork.Book.GetBookById(request.BookId);

            if (existingBook is null)
                return Result<string>.Failure(BookErrors.NotFoundBook, ResultStatusCodeEnum.NotFound);

            var existingUser = await _unitOfWork.User.GetUserByIdAsync(request.UserId);

            if (existingUser is null)
                return Result<string>.Failure(UserErrors.NotFoundUser, ResultStatusCodeEnum.NotFound);

            var comment = new Comment(request.Id, request.Content, request.CommentDate, request.BookId, request.UserId);

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Comment.CreateCommentAsync(comment);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<string>.Success(comment.CommentDate.ToString());
        }
    }
}
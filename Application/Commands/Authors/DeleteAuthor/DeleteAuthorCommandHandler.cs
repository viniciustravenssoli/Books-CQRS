using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Commands.Authors.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Author.GetAuthorByIdAsync(request.Id);

            if (author is null)
                return Result<string>.Failure(AuthorErrors.NotFoundAuthor, ResultStatusCodeEnum.NotFound);

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Author.DeleteAsync(author);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<string>.Success(author.Id.ToString());
        }
    }
}
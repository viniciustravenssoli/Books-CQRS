using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var existingAuthor = await _unitOfWork.Author.GetAuthorByIdAsync(request.Id);

            if (existingAuthor is null)
            {
                return Result<string>.Failure(AuthorErrors.NotFoundAuthor, ResultStatusCodeEnum.NotFound);
            }

            existingAuthor.Name = request.NewName;

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Author.UpdateAsync(existingAuthor);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<string>.Success(existingAuthor.Id.ToString());
        }
    }
}
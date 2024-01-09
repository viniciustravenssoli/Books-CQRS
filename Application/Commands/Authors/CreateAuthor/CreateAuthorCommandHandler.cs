using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Commands.Authors.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            Author author = new(request.Id, request.Name);

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Author.CreateAuthorAsync(author);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(author.Id);
        }
    }
}
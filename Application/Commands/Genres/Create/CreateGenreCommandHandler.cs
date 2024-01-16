using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Authors.CreateAuthor;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Commands.Genres
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateGenreCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            Genre genre = new(request.Id, request.Name);

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Genre.CreateGenreAsync(genre);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(genre.Id);
        }
    }
}
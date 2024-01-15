using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Commands.Genres.Delete
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGenreCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _unitOfWork.Genre.GetGenreByIdAsync(request.Id);

            if (genre == null)
                return Result<string>.Failure(GenreErrors.NotFoundGenre, ResultStatusCodeEnum.NotFound);

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Genre.DeleteAsync(genre);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<string>.Success(genre.Id.ToString());
        }
    }
}
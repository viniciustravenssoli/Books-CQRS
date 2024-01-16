using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Queries.Genre.GetAllGenre
{
    public class GetAllGenreQueryHandler : IRequestHandler<GetAllGenreQuery, Result<List<GenreViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllGenreQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GenreViewModel>>> Handle(GetAllGenreQuery request, CancellationToken cancellationToken)
        {
            var genreList = await _unitOfWork.Genre.GetAllAsync(request.PageQuery.Top, request.PageQuery.Skip);

            var genreViewModelList = genreList
                .Select(a => (GenreViewModel)a)
                .OrderByDescending(g => g.BooksQuantity)
                .ToList();

            return Result<List<GenreViewModel>>.Success(genreViewModelList);
        }
    }
}
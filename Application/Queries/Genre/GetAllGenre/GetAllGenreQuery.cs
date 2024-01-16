using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Helpers;
using Domain.Result;
using MediatR;

namespace Application.Queries.Genre.GetAllGenre
{
    public class GetAllGenreQuery : IRequest<Result<List<GenreViewModel>>>
    {
        public PageQuery PageQuery { get; set; }

        public GetAllGenreQuery(PageQuery pageQuery)
        {
            PageQuery = pageQuery;
        }
    }
}
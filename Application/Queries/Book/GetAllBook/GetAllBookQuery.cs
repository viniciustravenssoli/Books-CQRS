using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Helpers;
using Domain.Result;
using MediatR;

namespace Application.Queries.Book.GetAllBook
{
    public class GetAllBookQuery : IRequest<Result<List<BookViewModel>>>
    {
        public PageQuery PageQuery { get; set; }

        public GetAllBookQuery(PageQuery pageQuery)
        {
            PageQuery = pageQuery;
        }
    }
}
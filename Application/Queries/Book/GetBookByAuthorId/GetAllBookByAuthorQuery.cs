using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Helpers;
using Domain.Result;
using MediatR;

namespace Application.Queries.Book.GetBookByAuthor
{
    public class GetAllBookByAuthorQuery : IRequest<Result<List<BookViewModel>>>
    {
        public GetAllBookByAuthorQuery(PageQuery pageQuery, int id)
        {
            PageQuery = pageQuery;
            Id = id;
        }

        public PageQuery PageQuery { get; set; }
        public int Id { get; set; }
    }
}
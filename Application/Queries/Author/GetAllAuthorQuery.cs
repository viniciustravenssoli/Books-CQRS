using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Helpers;
using Domain.Result;
using MediatR;

namespace Application.Queries.Author
{
    public class GetAllAuthorQuery : IRequest<Result<List<AuthorViewModel>>>
    {
        public GetAllAuthorQuery(PageQuery pageQuery)
        {
            PageQuery = pageQuery;
        }
        public PageQuery PageQuery { get; set; }
    }
}
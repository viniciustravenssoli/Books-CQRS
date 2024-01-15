using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Helpers;
using Domain.Result;
using MediatR;

namespace Application.Queries.Author.GetAuthorsByName
{
    public class GetAuthorsByNameQuery : IRequest<Result<List<AuthorViewModel>>>
    {
        public GetAuthorsByNameQuery(string name, PageQuery pageQuery)
        {
            Name = name;
            PageQuery = pageQuery;
        }

        public string Name { get; set; }
        public PageQuery PageQuery { get; set; }
    }
}
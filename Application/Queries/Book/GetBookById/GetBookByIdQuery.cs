using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Result;
using MediatR;

namespace Application.Queries.Book.GetBookById
{
    public class GetBookByIdQuery : IRequest<Result<BookViewModel>>
    {
        public GetBookByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
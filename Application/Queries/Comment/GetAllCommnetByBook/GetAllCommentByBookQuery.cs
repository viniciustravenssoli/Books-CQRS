using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Helpers;
using Domain.Result;
using MediatR;

namespace Application.Queries.Comment.GetAllCommnetByBook
{
    public class GetAllCommentByBookQuery : IRequest<Result<List<CommentViewModel>>>
    {
        public PageQuery PageQuery { get; set; }
        public int BookId { get; set; }

        public GetAllCommentByBookQuery(PageQuery pageQuery, int bookId)
        {
            PageQuery = pageQuery;
            BookId = bookId;
        }
    }
}
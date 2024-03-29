using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Result;
using MediatR;

namespace Application.Commands.Comments.PostComment
{
    public class CreateCommentCommand : IRequest<Result<string>>
    {
        public CreateCommentCommand(int id, string content, DateTime commentDate, int bookId)
        {
            Id = id;
            Content = content;
            CommentDate = commentDate;
            BookId = bookId;
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public int BookId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.UtcNow;
        public string UserName { get; set; }

        public static implicit operator CommentViewModel(Comment comment)
        {
            if (comment is null)
                return null;
            return new()
            {
                Id = comment.Id,
                CommentDate = comment.CommentDate,
                Content = comment.Content,
                UserName = comment.User.UserName
            };
        }
    }
}
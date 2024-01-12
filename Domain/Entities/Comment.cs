

namespace Domain.Entities
{
    public class Comment : Base
    {
        public Comment(int id, string content, DateTime commentDate, int bookId, string userId)
        {
            Id = id;
            Content = content;
            CommentDate = commentDate;
            BookId = bookId;
            UserId = userId;
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.UtcNow;
        public int BookId { get; set; }
        public string UserId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}

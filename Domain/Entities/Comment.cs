

namespace Domain.Entities
{
    public class Comment : Base
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public int BookId { get; set; }
        public string UserId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}

using System.Text.Json.Serialization;


namespace Domain.Entities
{
    public class Genre : Base
    {
        public Genre()
        {
;
        }
        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Book>? Books { get; set; }

        public int CountBooks()
        {
            return Books?.Count() ?? 0;
        }
    }
}

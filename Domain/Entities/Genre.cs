using System.Text.Json.Serialization;


namespace Domain.Entities
{
    public class Genre : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Book>? Books { get; set; }
    }
}

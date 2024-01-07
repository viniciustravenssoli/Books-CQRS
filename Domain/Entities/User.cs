using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Comment>? Comments { get; set; }
    }
}

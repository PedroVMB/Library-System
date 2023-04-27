using Microsoft.AspNetCore.Identity;

namespace Library_System.Models
{
    public class UserModel : IdentityUser
    {
        public DateTime Birthday { get; set; }

        public UserModel() : base() { }
    }
}

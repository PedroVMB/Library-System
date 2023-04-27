using System.ComponentModel.DataAnnotations;

namespace Library_System.Data.Dtos
{
    public class LoginUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}

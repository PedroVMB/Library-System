using System.ComponentModel.DataAnnotations;

namespace Library_System.Data.Dtos
{
    public class CreateBookDto
    {
        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Publication { get; set; }
    }
}

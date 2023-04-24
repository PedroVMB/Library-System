using System.ComponentModel.DataAnnotations;

namespace Library_System.Data.Dtos;

public class UpdateBookDto
{

    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public string ISBN { get; set; }
}

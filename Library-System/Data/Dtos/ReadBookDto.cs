using System.ComponentModel.DataAnnotations;

namespace Library_System.Data.Dtos;

public class ReadBookDto
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    public string Publication { get; set; }

    public DateTime DateTime { get; set; }
}

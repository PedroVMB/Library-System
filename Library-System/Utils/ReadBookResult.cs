using Library_System.Data.Dtos;

namespace Library_System.Utils
{
    public class ReadBookResult
    {
        public bool IsSuccess { get; set; }
        public ReadBookDto Book { get; set; }
        public string ErrorMessage { get; set; }
    }
}

using Library_System.Data;
using Library_System.Data.Dtos;
using Library_System.Models;
using Library_System.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_System.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;
    private LibraryDbContext _dbContext;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }
    [HttpPost]
    public IActionResult RegisterBook([FromBody]CreateBookDto createBookDto)
    {
        var result = _bookService.RegisterBook(createBookDto);
        return result;
    }
    
    [HttpGet]
    public  IEnumerable<BookModel> ReadBook()
    {
        List<BookModel> book = (List<BookModel>)_bookService.ReadBooks();

        return book;
    }

    [HttpGet("{id}")]
    public IActionResult ReadBooks(Guid id)
    {
        return (IActionResult)_bookService.ReadBookForId(id);
    }
    [HttpPost("{id}")]
    public IActionResult UpdateBook([FromBody] UpdateBookDto bookModel, Guid id)
    {
        return (IActionResult)_bookService.UpdateBook(id, bookModel);
        
    }
    [HttpDelete]
    public IActionResult DeleteBook(Guid id)
    {
        return (IActionResult)_bookService.DeleteBook(id);
    }


}

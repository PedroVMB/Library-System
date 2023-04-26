using AutoMapper;
using Library_System.Data;
using Library_System.Data.Dtos;
using Library_System.Models;
using Library_System.Services;
using Library_System.Utils;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Library_System.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;
    private readonly IMapper _mapper; 
    private readonly LibraryDbContext _context;

    public BookController(BookService bookService, IMapper mapper, LibraryDbContext context)
    {
        _bookService = bookService;
        _mapper = mapper;
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> AddBook([FromBody] CreateBookDto bookDto)
    {
        var bookModel = await _bookService.RegisterBook(bookDto);

        return Ok(bookModel);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadBookDto>>> GetBooks([FromQuery] int skip = 0, [FromQuery] int take = 30)
    {
        var book = await _bookService.ReadBooks(skip, take);

        return Ok(_mapper.Map<List<ReadBookDto>>(book));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ReadBookDto>> GetById(Guid id)
    {
        var book = await _bookService.ReadBookForId(id);
        if (book == null) return NotFound();

        return Ok(book);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<ReadBookResult>> UpdateBook(Guid id, [FromBody] UpdateBookDto bookDto)
    {
        var result = await _bookService.UpdateBook(id, bookDto);

        if (!result.IsSuccess) return NotFound(result.ErrorMessage);

        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<ReadBookResult>> UpdatedBook(Guid id, [FromBody] UpdateBookDto bookDto)
    {
        var result = await _bookService.UpdateBook(id, bookDto);

        if (!result.IsSuccess) return NotFound(result.ErrorMessage);

        return Ok();
    }

    [HttpPatch("{id}")]
    public async Task<ReadBookResult> UpdateFilmeParcial(Guid id, JsonPatchDocument<UpdateBookDto> patch)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return new ReadBookResult { IsSuccess = false, ErrorMessage = "Filme not found" };
        }

        var BookToUpdate = _mapper.Map<UpdateBookDto>(book);
        patch.ApplyTo(BookToUpdate, ModelState);

        if (!TryValidateModel(BookToUpdate))
        {
            return new ReadBookResult { IsSuccess = false, ErrorMessage = "Invalid model" };
        }

        _mapper.Map(BookToUpdate, book);

        try
        {
            await _context.SaveChangesAsync();
            return new ReadBookResult { IsSuccess = true };
        }
        catch (Exception ex)
        {
            return new ReadBookResult { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteBook(Guid id)
    {
        return await _bookService.DeleteBook(id);
    }

}

using AutoMapper;
using Library_System.Data;
using Library_System.Data.Dtos;
using Library_System.Models;
using Library_System.Utils;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Library_System.Services;

public class BookService
{
    private IMapper _mapper;
    private LibraryDbContext _context;
    private readonly Action<JsonPatchError> ModelState;

    public BookService(IMapper mapper, LibraryDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public IActionResult RegisterBook(CreateBookDto dto)
    {
        BookModel bookModel = _mapper.Map<BookModel>(dto);

        _context.Books.Add(bookModel);
        _context.SaveChanges();

        return (IActionResult)dto;
    }

    public IEnumerable<ReadBookDto> ReadBooks([FromQuery] int skip = 0, [FromQuery] int take = 35)
    {
        return _mapper.Map<List<ReadBookDto>>(_context.Books.Skip(skip).Take(take).ToList());
    }

    public  ReadBookResult ReadBookForId(Guid id)
    {
        var book = _context.Books.FirstOrDefault(book => book.Id == id);
        if (book == null)
        {
            return new ReadBookResult { IsSuccess = false, ErrorMessage = "Book not found" };
        }
        var bookDto = _mapper.Map<ReadBookDto>(book);
        return new ReadBookResult { IsSuccess = true, Book = bookDto };

    }

    public ReadBookResult UpdateBook(Guid id, [FromBody] UpdateBookDto bookDto)
    {
        var book = _context.Books.FirstOrDefault(book => book.Id == id);
        if(book == null)
        {
            return new ReadBookResult { IsSuccess = false, ErrorMessage = "Book not found" };
        }
        _mapper.Map(bookDto, book);
        _context.SaveChanges();
        return new ReadBookResult { IsSuccess = true};

    }

    public ReadBookResult DeleteBook(Guid id)
    {
        var book = _context.Books.FirstOrDefault(_book => _book.Id == id);
        if (book == null) return new ReadBookResult { IsSuccess = false, ErrorMessage = "Book not found" };

        _context.Remove(book);
        _context.SaveChanges();
        return new ReadBookResult { IsSuccess = true };

    } 
    
}

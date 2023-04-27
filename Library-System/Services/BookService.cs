using AutoMapper;
using Library_System.Data;
using Library_System.Data.Dtos;
using Library_System.Models;
using Library_System.Utils;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Library_System.Services;

public class BookService
{
    private readonly IMapper _mapper;
    private readonly LibraryDbContext _context;

    public BookService(IMapper mapper, LibraryDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<BookModel> RegisterBook(CreateBookDto dto)
    {
        BookModel bookModel = _mapper.Map<BookModel>(dto);

        _context.Books.Add(bookModel);
        _context.SaveChanges();

        return bookModel;
    }

    public async Task<List<BookModel>> ReadBooks(int skip, int take)
    {
        return await _context.Books.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<ReadBookDto> ReadBookForId(Guid id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        var dto = _mapper.Map<ReadBookDto>(book);

        return dto;
    }

    public async Task<ReadBookResult> UpdateBook(Guid id, UpdateBookDto bookDto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return new ReadBookResult { IsSuccess = false, ErrorMessage = "Book not found" };
        }

        _mapper.Map(bookDto, book);

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

    public async Task<bool> DeleteBook(Guid id)
    {
        var book = _context.Books.FirstOrDefault(_book => _book.Id == id);
        if (book == null) throw new Exception($"Book with this id {id} is not found");

        _context.Books.Remove(book);
        _context.SaveChanges();

        return true;
    } 
    
}

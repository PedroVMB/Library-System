using AutoMapper;
using Library_System.Data.Dtos;
using Library_System.Models;

namespace Library_System.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<CreateBookDto, BookModel>();
        CreateMap<UpdateBookDto, BookModel>();
        CreateMap<BookModel, ReadBookDto>();
    }
}

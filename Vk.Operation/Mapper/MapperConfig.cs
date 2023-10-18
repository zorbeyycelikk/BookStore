using AutoMapper;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<BookCreateRequest, Book >();
        CreateMap<BookUpdateRequest, Book >();
        CreateMap<Book, BookResponse>()
            .ForMember(dest => dest.AuthorName, opt => opt
                .MapFrom(src => src.Author.Name))
            .ForMember(dest => dest.CategoryName, opt => opt
                .MapFrom(src => src.Category.CategoryName));

        CreateMap<CategoryCreateRequest, Category >();
        CreateMap<CategoryUpdateRequest, Category >();
        CreateMap<Category, CategoryResponse >();
        
        CreateMap<AuthorCreateRequest, Author >();
        CreateMap<AuthorUpdateRequest, Author >();
        CreateMap<Author, AuthorResponse >();
    }
}
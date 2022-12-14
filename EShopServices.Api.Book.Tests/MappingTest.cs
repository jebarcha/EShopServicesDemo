using AutoMapper;
using EShopServices.Api.Book.Application;
using EShopServices.Api.Book.Model;

namespace EShopServices.Api.Book.Tests;

public class MappingTest : Profile
{
    public MappingTest()
    {
        CreateMap<MaterialBook, MaterialBookDto>();
    }
}

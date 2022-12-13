using AutoMapper;
using EShopServices.Api.Book.Model;

namespace EShopServices.Api.Book.Application;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<MaterialBook, MaterialBookDto>();
	}
}

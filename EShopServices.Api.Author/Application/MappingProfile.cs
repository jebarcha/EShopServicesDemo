using AutoMapper;
using EShopServices.Api.Author.Model;

namespace EShopServices.Api.Author.Application;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<AuthorBook, AuthorDto>();
	}
}

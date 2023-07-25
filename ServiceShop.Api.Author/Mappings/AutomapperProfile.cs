using AutoMapper;
using ServiceShop.Api.Author.Application;
using ServiceShop.Api.Author.Model;

namespace ServiceShop.Api.Author.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<BookAuthor, AuthorDto>();
        }
    }
}

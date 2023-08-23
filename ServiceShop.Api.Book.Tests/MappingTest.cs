using AutoMapper;
using ServiceShop.Api.Book.Application;
using ServiceShop.Api.Book.Model;

namespace ServiceShop.Api.Book.Tests
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<MaterialLibrary, MaterialLibraryDto>();
        }
    }
}

using AutoMapper;
using ServiceShop.Api.Book.Application;
using ServiceShop.Api.Book.Model;

namespace ServiceShop.Api.Book.Mappings
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<MaterialLibrary, MaterialLibraryDto>();
        }
    }
}

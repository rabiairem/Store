using AutoMapper;
using StoreServiceAPI.Models;
using StoreServiceAPI.Entities;

namespace StoreServiceAPI.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Store, StoreDTO>().ReverseMap();
        }
    }
}

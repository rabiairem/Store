using AutoMapper;
using StoreServiceAPI.Models;
using StoreServiceAPI.DataAccess.Entities;

namespace StoreServiceAPI.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Store, StoreDTO>().ForMember(store => store.SapNumber, s => s.MapFrom(src => src.SapNumber_id)).ReverseMap();
        }
    }
}
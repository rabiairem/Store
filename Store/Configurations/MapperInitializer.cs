using AutoMapper;
using StoreServiceAPI.Models;
using StoreServiceAPI.Entities;

namespace StoreServiceAPI.Configurations
{
    public class MapperInitializer
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Store, StoreDTO>().ForMember(store => store.SapNumber, s => s.MapFrom(src => src.SapNumber_id)).ReverseMap();
            });

            return mappingConfig;
        }
    }
}
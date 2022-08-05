using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using StoreServiceAPI.Configurations;
using System.Reflection;
using System.Text;

namespace StoreServiceAPI
{
    public static class ServiceExtensions
    {
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");//header ile belirtilebilir
            });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            IMapper mapper = MapperInitializer.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}

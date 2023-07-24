using CommandsService.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;

namespace CommandsService.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void ConfigurePlatformDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase($"PlatformDb_{DateTime.Now.ToFileTimeUtc()}"));
            //services.AddDbContext<PlatformDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<ICommandRepo, CommandRepo>();
        }
        //public static void ConfigureHttpClient(this IServiceCollection services)
        //{
        //    services.AddScoped<ICommandDataClient, HttpCommandDataClient>();
        //}
    }
}

using CommandsService.AsyncDataServices;
using CommandsService.Data;
using CommandsService.EventProcessing;
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
        public static void ConfigureEventProcess(this IServiceCollection services)
        {
            services.AddSingleton<IEventProcessor, EventProcessor>();
        }
        public static void ConfigureMessageBus(this IServiceCollection services)
        {
            services.AddHostedService<MessageBusSubscriber>();
        }

        //public static void ConfigureHttpClient(this IServiceCollection services)
        //{
        //    services.AddScoped<ICommandDataClient, HttpCommandDataClient>();
        //}
    }
}

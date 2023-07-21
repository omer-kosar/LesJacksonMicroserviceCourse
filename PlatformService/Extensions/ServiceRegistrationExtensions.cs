﻿using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Interfaces;
using PlatformService.SyncDataServices.Http;
using System;
using System.Runtime.CompilerServices;

namespace PlatformService.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void ConfigurePlatformDbContext(this IServiceCollection services)
        {
            services.AddDbContext<PlatformDbContext>(opt => opt.UseInMemoryDatabase($"PlatformDb_{DateTime.Now.ToFileTimeUtc()}"));
            //services.AddDbContext<PlatformDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IPlatformRepostiory, PlatformRepository>();
        }
        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddScoped<ICommandDataClient, HttpCommandDataClient>();
        }
    }
}

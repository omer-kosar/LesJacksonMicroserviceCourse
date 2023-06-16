using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Interfaces;
using System.Runtime.CompilerServices;

namespace PlatformService.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void ConfigurePlatformDbContext(this IServiceCollection services)
        {
            services.AddDbContext<PlatformDbContext>(opt => opt.UseInMemoryDatabase($"PlatformDb_{DateTime.Now:dd:MM:yyyy HH:mm:ss}"));
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IPlatformRepostiory, PlatformRepository>();
        }
    }
}

using PlatformService.Models;

namespace PlatformService.Data.Interfaces
{
    public interface IPlatformRepostiory
    {
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Platform>> GetAllPlatforms();
        Task<Platform> GetPlatformById(int id);
        Task CreatePlatform(Platform platform);
    }
}

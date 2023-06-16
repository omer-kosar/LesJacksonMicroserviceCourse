﻿namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<PlatformDbContext>());
            }
        }

        private static void SeedData(PlatformDbContext dbContext)
        {
            if (!dbContext.Platforms.Any())
            {
                dbContext.Platforms.AddRange(
                    new Models.Platform
                    {
                        Name = "Dot Net",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Models.Platform
                    {
                        Name = "Sql Server Express",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Models.Platform
                    {
                        Name = "Kubernetes",
                        Publisher = "Cloud Native Computing Foundation",
                        Cost = "Free"
                    });

                dbContext.SaveChanges();
            }
        }
    }
}

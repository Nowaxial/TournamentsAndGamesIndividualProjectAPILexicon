using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;

namespace Tournament.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var db = serviceProvider.GetRequiredService<TournamentContext>();
            await db.Database.MigrateAsync();

            try
            {
                await SeedData.Initialize(db);
            }
            catch (Exception ex)
            {
                // Logga eventuellt fel här
                Console.WriteLine($"Fel vid seeding: {ex.Message}");
            }
        }
    }
}
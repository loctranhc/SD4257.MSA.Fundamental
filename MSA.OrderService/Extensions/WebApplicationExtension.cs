using MSA.OrderService.Infrastructure.Data;

namespace MSA.OrderService.Extensions
{
    public static class WebApplicationExtension
    {
        public static void MigrateDbContext(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MainDbContext>();
            context.Database.EnsureCreated();
        }
    }
}
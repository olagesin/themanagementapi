using Microsoft.EntityFrameworkCore;
using Repositories;

namespace MovieManagementAPI.Configurations
{
    public static class DataContextSetup
    {
        public static void ConfigurePostgresContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(option =>
        option.UseNpgsql(configuration.GetConnectionString("postgresConnection"),
        c => c.MigrationsAssembly("MovieManagementAPI")));
    }
}

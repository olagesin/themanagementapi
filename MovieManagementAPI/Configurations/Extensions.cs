using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;

namespace MovieManagementAPI.Configurations
{
    public static class Extensions
    {
        public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}

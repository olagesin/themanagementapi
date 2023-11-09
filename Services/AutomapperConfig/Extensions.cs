using Microsoft.Extensions.DependencyInjection;

namespace Services.AutomapperConfig
{
    public static class Extensions
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(Extensions));
        }
    }
}

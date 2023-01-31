using Microsoft.Extensions.Hosting;

namespace Ukraine.Infrastructure.Hosting.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ValidateServicesOnBuild(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                options.ValidateOnBuild = true;
            });
            
            return hostBuilder;
        }
    }
}

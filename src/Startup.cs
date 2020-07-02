using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using test.Logic;

namespace test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<ProcessingService>();
            services.AddSingleton<ParsingService>();
            services.AddSingleton<PathBuildingService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseRouting();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.ConfigureRoute();
        }
    }
}

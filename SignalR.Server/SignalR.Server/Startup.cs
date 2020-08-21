using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Server.Hubs;

namespace SignalR.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
            => services.AddCors(options =>
                {
                    options.AddPolicy(name: Params.CorsPolicyName,
                        builder =>
                        {
                            builder.AllowAnyHeader()
                                .WithOrigins(Params.Origins)
                                .SetIsOriginAllowedToAllowWildcardSubdomains()
                                .AllowCredentials()
                                .WithMethods(Params.Methods);
                    });
            })
            .AddSignalR();
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
            => app.UseRouting()
                  .UseCors(Params.CorsPolicyName)
                  .UseEndpoints(endpoints => { endpoints.MapHub<ChatHub>("/ChatHub"); });
    }
}

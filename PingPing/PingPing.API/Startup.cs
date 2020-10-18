using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PingPing.NVDCollector.Abstract;
using PingPing.NVDCollector.Concrete;
using PingPing.NVDParser.Abstract;
using PingPing.NVDParser.Concrete;
using PingPing.SpyShark;

namespace PingPing.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<INVDParserManager, NVDParserManager>();
            services.AddSingleton<IAffectedCPEParser, AffectedCPEParser>();
            services.AddSingleton<ICPEListParser, CPEListParser>();
            services.AddSingleton<ICVEListParser, CVEListParser>();
            services.AddSingleton<IDetailsParser, DetailsParser>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

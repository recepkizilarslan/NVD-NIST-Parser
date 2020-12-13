using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NVDParser.Parser;
using NVDParser.Parser.Abstract;
using NVDParser.Parser.Concrete;
using Microsoft.OpenApi.Models;

namespace NVDParser.API
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "NVD Parser Endpoints",
                    Description = "This services provides to get all public vulnerabilities detail.",
                    Contact = new OpenApiContact
                    {
                        Name = "Recep Kızılarslan",
                        Email = "recep.kizilarslan@invicti.com",
                    }
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NVD-NIST-Prser");
            });

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

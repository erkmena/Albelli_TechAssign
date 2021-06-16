using Albelli_TechAssign.Filters;
using Albelli_TechAssign.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using System.Text.Json;

namespace Albelli_TechAssign
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
            services.AddLogging(s => s.AddConsole());
            services.AddControllers(o => {
                // Remove Unnecessary Formats
                o.InputFormatters.RemoveType<XmlDataContractSerializerInputFormatter>();
                o.InputFormatters.RemoveType<XmlSerializerInputFormatter>();
                o.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                o.OutputFormatters.RemoveType<StreamOutputFormatter>();
                o.OutputFormatters.RemoveType<StringOutputFormatter>();
                o.OutputFormatters.RemoveType<XmlDataContractSerializerOutputFormatter>();
                o.OutputFormatters.RemoveType<XmlSerializerOutputFormatter>();

                o.Filters.Add<GlobalExceptionFilter>(); //Use Global Exception Handling
            }).AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.IgnoreNullValues = true;
                o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal); // Use Gzip for zipping response to improve Performance

            services.AddResponseCompression();
            ConfigureDI.ConfigureDependencyInjections(services);//Add DI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Albelli_TechAssign", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureDB configure = new ConfigureDB(Configuration);
            configure.Configure();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Albelli_TechAssign v1"));
            }

            app.UseResponseCompression(); //Using Gzip

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

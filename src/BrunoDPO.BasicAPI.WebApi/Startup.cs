using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using BrunoDPO.BasicAPI.Application.Validators;
using BrunoDPO.BasicAPI.WebApi.Options;
using BrunoDPO.BasicAPI.WebApi.Routing;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

namespace BrunoDPO.BasicAPI.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>();

            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(null, allowIntegerValues: true));
            });

            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            }).AddValidatorsFromAssemblyContaining<PersonValidator>();
            ValidatorOptions.Global.LanguageManager.Enabled = true;

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddCheck("ready", () => HealthCheckResult.Healthy());

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(config =>
            {
                config.EnableAnnotations();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting()
                .UseCors(e =>
                {
                    e.AllowAnyOrigin();
                    e.AllowAnyMethod();
                    e.AllowAnyHeader();
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/ready", new HealthCheckOptions { Predicate = r => r.Tags.Contains("services") })
               .UseHealthChecks("/self", new HealthCheckOptions { Predicate = r => r.Name.Contains("self") });
        }
    }
}

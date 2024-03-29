﻿using BrunoDPO.BasicAPI.Application.Validators;
using BrunoDPO.BasicAPI.WebApi.Filter;
using BrunoDPO.BasicAPI.WebApi.Options;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace BrunoDPO.BasicAPI.WebApi
{
    public class Startup
    {
        private static readonly string apiTitle = string.Join('.', typeof(Program).Namespace.Split('.')[..^1]);

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
            }).AddNewtonsoftJson(options =>
            {
                options.UseCamelCasing(true);
                options.SerializerSettings.Converters.Add(new StringEnumConverter(new KebabCaseNamingStrategy(), allowIntegerValues: true));
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
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(config =>
            {
                config.EnableAnnotations();
            }).AddSwaggerGenNewtonsoftSupport();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
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

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    var notice = description.IsDeprecated ? " (deprecated)" : string.Empty;
                    options.SwaggerEndpoint(
                        $"../swagger/{description.GroupName}/swagger.json",
                        $"{apiTitle} {description.GroupName}{notice}");
                }
                options.DocExpansion(DocExpansion.List);
            });

            app.UseHealthChecks("/ready", new HealthCheckOptions { Predicate = r => r.Tags.Contains("services") })
               .UseHealthChecks("/self", new HealthCheckOptions { Predicate = r => r.Name.Contains("self") });
        }
    }
}

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BrunoDPO.BasicAPI.WebApi.Options
{
    public class SwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private static readonly string apiTitle = string.Join('.', typeof(Program).Namespace.Split('.')[..^1]);

        private readonly IApiVersionDescriptionProvider _provider;

        public SwaggerOptions(IApiVersionDescriptionProvider provider) =>
            _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var deprecated = description.IsDeprecated ? " This API version has been deprecated." : string.Empty;
            return new OpenApiInfo
            {
                Title = apiTitle,
                Description = $"Basic API using Swagger and Version Management.{deprecated}",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact
                {
                    Name = "Author",
                    Email = "a@non.com"
                }
            };
        }
    }
}

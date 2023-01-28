using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Enums;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime;

public class CustomHeaderSwaggerAttribute : IOperationFilter
{
    private readonly ConfigurationManager _configurationManager;

    public CustomHeaderSwaggerAttribute(ConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        string? headerKey = _configurationManager["CustomHeaderKey"];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = headerKey,
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Description = $"Possible values: {ConnectionNames.Connection0}, {ConnectionNames.Connection1}"
            }
        });
    }

}

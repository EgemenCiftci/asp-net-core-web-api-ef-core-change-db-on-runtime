using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime;

public class CustomHeaderSwaggerAttribute : IOperationFilter
{
    private readonly IConfiguration _configuration;
    private readonly ConnectionService _connectionService;

    public CustomHeaderSwaggerAttribute(IConfiguration configuration,
                                        ConnectionService connectionService)
    {
        _configuration = configuration;
        _connectionService = connectionService;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = _configuration["CustomHeaderKey"],
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Description = $"Possible values: {string.Join(", ", _connectionService.GetConnections())}"
            }
        });
    }

}

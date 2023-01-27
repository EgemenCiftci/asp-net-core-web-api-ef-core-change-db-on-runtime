using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime;

public class CustomHeaderSwaggerAttribute : IOperationFilter
{
    public static string HeaderKey = "connection-name";

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = HeaderKey,
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }

}

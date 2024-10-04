using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JobScial.WebAPI
{
    public class ODataQueryOptionsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!context.ApiDescription.RelativePath.StartsWith("odata"))
            {
                return;
            }


            var queryOptions = new[] { "$filter", "$orderby", "$select", "$top", "$skip", "$expand", "$count" };


            foreach (var option in queryOptions)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = option,
                    In = ParameterLocation.Query,
                    Required = false,
                    Description = $"OData query option: {option}",
                    Schema = new OpenApiSchema
                    {
                        Type = "string"
                    }
                });
            }
        }
    }
}

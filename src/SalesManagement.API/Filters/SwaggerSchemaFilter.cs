using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class SwaggerSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema?.Properties == null || context.Type == null)
            return;

        var requiredProperties = context.Type.GetProperties()
            .Where(prop => prop.GetCustomAttributes(typeof(RequiredAttribute), true).Any())
            .Select(prop => prop.Name);

        foreach (var requiredProp in requiredProperties)
        {
            var key = schema.Properties.Keys.FirstOrDefault(k => k.Equals(requiredProp, StringComparison.OrdinalIgnoreCase));
            if (key != null)
                schema.Required.Add(key);
        }
    }
}

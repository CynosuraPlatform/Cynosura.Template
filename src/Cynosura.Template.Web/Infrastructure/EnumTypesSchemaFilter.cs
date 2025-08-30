using Cynosura.Template.Core.Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Cynosura.Template.Web.Infrastructure
{
    public class EnumTypesSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Enum != null && schema.Enum.Count > 0 &&
                context.Type != null && context.Type.IsEnum)
            {
                schema.Description += "<p>Values:</p><ul>";

                foreach (var enumValue in Enum.GetValues(context.Type).Cast<Enum>())
                {
                    schema.Description += $"<li><i>{Convert.ToInt32(enumValue)}</i> - {enumValue.GetDescriptionFromEnumValue()}</li>";
                }

                schema.Description += "</ul>";
            }
        }
    }
}

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zademy.Api.Filters;

public class CustomOrderingDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths
            .OrderBy(x => GetSortKey(x.Key, x.Value))
            .ToList();

        swaggerDoc.Paths.Clear();
        foreach (var path in paths)
        {
            swaggerDoc.Paths.Add(path.Key, path.Value);
        }
    }

    private static string GetSortKey(string path, OpenApiPathItem pathItem)
    {
        var tag = pathItem.Operations.Values
            .SelectMany(op => op.Tags)
            .FirstOrDefault()?.Name ?? "Other";

        var groupOrder = tag switch
        {
            "Courses" => "1",
            "Students" => "2",
            "Course Instances" => "3",
            "Grades" => "4",
            _ => "5"
        };

        var operation = pathItem.Operations.First();
        var methodOrder = operation.Key switch
        {
            OperationType.Get => "1",
            OperationType.Post => "2",
            OperationType.Put => "3",
            OperationType.Delete => "4",
            _ => "5"
        };

        // Within each method type, non-parameterized paths come first
        var hasParams = path.Contains('{');
        var pathPriority = hasParams ? "2" : "1";

        return $"{groupOrder}_{methodOrder}_{pathPriority}_{path}";
    }
}
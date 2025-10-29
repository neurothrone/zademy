namespace Zademy.Api.Configuration;

public static class ApiVersioning
{
    public const string Version = "v1";
    private const string Prefix = "/api";
    
    public static string RoutePrefix => $"{Prefix}/{Version}";
}
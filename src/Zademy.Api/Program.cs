using Zademy.Api.Configuration;
using Zademy.Api.Extensions;
using Zademy.Api.Middlewares;
using Zademy.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddZademyDatabase();
builder.Services.AddZademyIdentity();
builder.Services.AddZademyServices();
builder.Services.AddZademySwagger();
builder.Services.AddZademyHealthChecks();

var app = builder.Build();

app.Services.SetupDatabase(seedData: true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            url: $"/swagger/{ApiVersioning.DocName}/swagger.json",
            name: $"Zademy API {ApiVersioning.SemanticName}"
        );
        // options.RoutePrefix = string.Empty; // Sets Swagger UI to be served at the root
    });
}

app.UseHttpsRedirection();

app.UseZademyIdentity();
app.UseMiddleware<UserDeletionAuthorizationMiddleware>();
app.MapZademyHealthChecks();
app.MapZademyEndpoints();

app.Run();
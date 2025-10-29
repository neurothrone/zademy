using Zademy.Api.Configuration;
using Zademy.Api.Extensions;
using Zademy.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddZademyDatabase();
builder.Services.AddZademyIdentity();
builder.Services.AddZademyServices();
builder.Services.AddZademySwagger();

var app = builder.Build();

app.Services.SetupDatabase(seedData: true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(
            url: $"/swagger/{ApiVersioning.DocName}/swagger.json",
            name: $"Zademy API {ApiVersioning.SemanticName}"
        );
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapGet("/", context =>
{
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.UseZademyIdentity();
app.MapZademyEndpoints();

app.Run();
using Microsoft.EntityFrameworkCore;
using Zademy.Api.Endpoints;
using Zademy.Business.Services;
using Zademy.Business.Services.Contracts;
using Zademy.Persistence.Data;
using Zademy.Persistence.Entities;
using Zademy.Persistence.Repositories;
using Zademy.Persistence.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ZademyDbContext>(options =>
{
    options.UseInMemoryDatabase("ZademyDb");

#if DEBUG
    options.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Services.SeedInMemoryDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapGet("/", context =>
{
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.MapCourseEndpoints();
app.MapStudentEndpoints();

app.Run();
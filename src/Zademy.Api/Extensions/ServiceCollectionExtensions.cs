using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Zademy.Api.Configuration;
using Zademy.Api.Filters;
using Zademy.Business.Services;
using Zademy.Business.Services.Contracts;
using Zademy.Persistence.Data;
using Zademy.Persistence.Entities;
using Zademy.Persistence.Repositories;
using Zademy.Persistence.Repositories.Contracts;

namespace Zademy.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddZademyDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ZademyAppDbContext>(options =>
        {
            options.UseInMemoryDatabase("ZademyDb");

#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });

        services.AddDbContext<ZademyIdentityDbContext>(options =>
        {
            options.UseInMemoryDatabase("ZademyDb");

#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });
    }

    public static void AddZademyIdentity(this IServiceCollection services)
    {
        services
            .AddIdentityApiEndpoints<UserEntity>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ZademyIdentityDbContext>();

        services.AddAuthorization();
    }


    public static void AddZademyServices(this IServiceCollection services)
    {
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseService, CourseService>();

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentService, StudentService>();

        services.AddScoped<ICourseInstanceRepository, CourseInstanceRepository>();
        services.AddScoped<ICourseInstanceService, CourseInstanceService>();

        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<IGradeService, GradeService>();
    }

    public static void AddZademySwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(ApiVersioning.DocName, new OpenApiInfo
            {
                Title = "Zademy API",
                Description = "API for managing courses, students, course instances, and grades.",
                Version = ApiVersioning.SemanticName
            });

            options.DocumentFilter<CustomOrderingDocumentFilter>();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token in the text input below."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void MapZademyHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");
    }


    public static void AddZademyHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<ZademyAppDbContext>()
            .AddDbContextCheck<ZademyIdentityDbContext>()
            .AddCheck("Memory", () =>
            {
                var allocated = GC.GetTotalMemory(forceFullCollection: false);
                const long threshold = 1024L * 1024L * 1024L; // 1 GB
                return allocated < threshold
                    ? HealthCheckResult.Healthy()
                    : HealthCheckResult.Degraded($"Memory usage: {allocated / 1024 / 1024} MB");
            });
    }
}
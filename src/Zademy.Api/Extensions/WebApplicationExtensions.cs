using Zademy.Api.Endpoints.CourseInstances;
using Zademy.Api.Endpoints.Courses;
using Zademy.Api.Endpoints.Grades;
using Zademy.Api.Endpoints.Students;
using Zademy.Persistence.Entities;

namespace Zademy.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void UseZademyIdentity(this WebApplication app)
    {
        app.MapIdentityApi<UserEntity>();
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void MapZademyEndpoints(this WebApplication app)
    {
        app.MapCourseEndpoints();
        app.MapStudentEndpoints();
        app.MapCourseInstanceEndpoints();
        app.MapGradeEndpoints();
    }
}
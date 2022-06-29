using ApplicationServices.Services.Courses;
using ApplicationServices.Services.Students;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ICourseService, CourseService>();

    }
}
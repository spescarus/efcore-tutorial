using Application.Contexts.InstructorContexts;
using Application.Services.Courses;
using Application.Services.Instructors;
using Application.Services.Students;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped<IInstructorService, InstructorService>();

        services.AddScoped<GetInstructorDetailsContext, GetInstructorDetailsContext>();
        services.AddScoped<GetInstructorForUpdateContext, GetInstructorForUpdateContext>();
        services.AddScoped<CreateInstructorContext, CreateInstructorContext>();
        services.AddScoped<UpdateInstructorContext, UpdateInstructorContext>();
        services.AddScoped<DeleteInstructorContext, DeleteInstructorContext>();
    }
}
using Domain.RepositoryInterfaces;
using Domain.RepositoryInterfaces.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Generics;
using Persistence.Repositories;

namespace Persistence;

public static class ServiceCollectionExtensions
{
    public static void AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<EfCoreContext>(opt => opt.UseSqlServer(connectionString));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IInstructorRepository, InstructorRepository>();
    }
}
using System.Linq.Expressions;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.RepositoryInterfaces.Generics;
using Domain.Search;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public sealed class StudentRepository : RepositoryWithSearch<Student>, IStudentRepository
{
    public StudentRepository(EfCoreContext context)
        : base(context)
    {
    }

    public async Task<IPartialCollection<Student>> SearchStudentsAsync(SearchArgs searchArgs)
    {

        Expression<Func<Student, bool>>? predicate = null;

        if (!string.IsNullOrWhiteSpace(searchArgs.SearchText))
        {
            predicate = p => p.Name.FirstMidName.Contains(searchArgs.SearchText) ||
                             p.Name.LastName.Contains(searchArgs.SearchText);
        }


        Expression<Func<Student, object>> sortOrder;
        switch (searchArgs.SortOption.PropertyName)
        {
            case "LastName":
                sortOrder = p => p.Name.LastName;
                break;
            default:
                sortOrder = SortExpression(searchArgs, "Id");
                break;
        }

        return predicate == null
                   ? await GetAllAsync(searchArgs.SortOption.SortOrder, sortOrder, searchArgs.Offset, searchArgs.Limit)
                   : await GetAllByAsync(predicate, searchArgs.SortOption.SortOrder, sortOrder, searchArgs.Offset, searchArgs.Limit);
    }

    public async Task<Student?> GetStudentByIdAsync(long studentId)
    {
        var student =  await GetByIdAsync(studentId);

        await Context.Entry(student)
               .Collection(p => p.Enrollments)
               .Query()
               .Include(p => p.Course)
               .LoadAsync();

        return student;
    }
}
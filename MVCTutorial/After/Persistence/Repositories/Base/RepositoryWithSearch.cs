using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Domain.Base;
using Domain.Search;
using Persistence.Context;

namespace Persistence.Repositories.Base;

public class RepositoryWithSearch<TEntity> : Repository<TEntity> where TEntity : Entity
{
    public RepositoryWithSearch(EfCoreContext context) : base(context)
    {
    }

    protected static Expression<Func<TEntity, object>> SortExpression(SearchArgs searchRequest, string defaultSearchField)
    {
        var sortPropertyName = string.IsNullOrWhiteSpace(searchRequest.SortOption.PropertyName)
            ? defaultSearchField
            : searchRequest.SortOption.PropertyName;

        var parameterExpression = Expression.Parameter(typeof(TEntity), nameof(TEntity));

        var convert = Expression.Convert(Expression.Property(parameterExpression, sortPropertyName), typeof(object));
        var sortExpression = Expression.Lambda<Func<TEntity, object>>(convert, parameterExpression);
        return sortExpression;
    }

    protected static string RemoveDiacritics(string stringIn)
    {
        if (string.IsNullOrWhiteSpace(stringIn))
        {
            return stringIn;
        }

        stringIn = stringIn.Trim()
            .ToLower();

        var stringFormD = stringIn.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var t in stringFormD)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(t);

            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(t);
            }
        }

        return (stringBuilder.ToString()
            .Normalize(NormalizationForm.FormC));
    }
}

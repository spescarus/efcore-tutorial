using Domain.Base;

namespace Domain.Entities.ValueObjects;

public class Name : ValueObject
{
    public string FirstMidName { get; }
    public string LastName     { get; }

    private Name(string firstMidName, string lastName)
    {
        FirstMidName = firstMidName;
        LastName     = lastName;
    }

    public static Result<Name> Create(string firstMidName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstMidName))
        {
            return Result.Failure<Name>("First name should not be empty");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure<Name>("Last name should not be empty");
        }

        firstMidName = firstMidName.Trim();
        lastName  = lastName.Trim();

        if (firstMidName.Length > 50)
        {
            return Result.Failure<Name>("First name cannot be longer than 50 characters.");
        }

        if (lastName.Length > 50)
        {
            return Result.Failure<Name>("Last name cannot be longer than 50 characters.");
        }

        return Result.Success(new Name(firstMidName, lastName));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstMidName;
        yield return LastName;
    }
}
using Domain.Base;

namespace Domain.Entities.ValueObjects;

public class PersonName : ValueObject
{
    public string FirstMidName { get; }
    public string LastName     { get; }

    private PersonName(string firstMidName, string lastName)
    {
        FirstMidName = firstMidName;
        LastName     = lastName;
    }

    public static Result<PersonName> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<PersonName>("First name should not be empty");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure<PersonName>("Last name should not be empty");
        }

        firstName = firstName.Trim();
        lastName  = lastName.Trim();

        if (firstName.Length > 50)
        {
            return Result.Failure<PersonName>("First name cannot be longer than 50 characters.");
        }

        if (lastName.Length > 50)
        {
            return Result.Failure<PersonName>("Last name cannot be longer than 50 characters.");
        }

        return Result.Success(new PersonName(firstName, lastName));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstMidName;
        yield return LastName;
    }
}
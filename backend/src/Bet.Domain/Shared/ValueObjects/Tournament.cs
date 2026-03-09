using CSharpFunctionalExtensions;

namespace Bet.Domain.Shared.ValueObjects;

public class Tournament : ValueObject
{
    //ef core
    private Tournament()
    {
        
    }
    public string Name { get; } = default!;
    public string Country { get; } = default!;

    private Tournament(string name, string country)
    {
        Name = name;
        Country = country;
    }

    public static Result<Tournament, Error> Create(string name, string country)
    {
        if (string.IsNullOrWhiteSpace(name) || 
            name.Length > Constants.General.MAX_NAME_LENGTH ||
            name.Length < Constants.General.MIN_NAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(Tournament));
        
        if (string.IsNullOrWhiteSpace(country) || 
            country.Length > Constants.General.MAX_NAME_LENGTH ||
            country.Length < Constants.General.MIN_NAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(Tournament));

        return new Tournament(name, country);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Country;
    }
}
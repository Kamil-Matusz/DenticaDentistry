using DenticaDentistry.Core.Exceptions;

namespace DenticaDentistry.Core.ValueObjects;

public sealed record Fullname
{
    public string Value { get; }

    public Fullname(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 3)
        {
            throw new InvalidFullNameException(value);
        }

        Value = value;
    }

    public static implicit operator Fullname(string value) => value is null ? null : new Fullname(value);

    public static implicit operator string(Fullname value) => value?.Value;

    public override string ToString() => Value;
}
using System.Text.RegularExpressions;
using DenticaDentistry.Core.Exceptions;

namespace DenticaDentistry.Core.ValueObjects;

public sealed record PhoneNumber
{
    private static readonly Regex Regex = new Regex(@"^\+?48(?:[-\s]?\d{3}){3}$", RegexOptions.Compiled);

    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPhoneNumberException(value);
        }

        if (!Regex.IsMatch(value))
        {
            throw new InvalidPhoneNumberException(value);
        }

        Value = value;
    }
    
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;

    public static implicit operator PhoneNumber(string phoneNumber) => new(phoneNumber);

    public override string ToString() => Value;
}
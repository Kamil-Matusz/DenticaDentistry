using DenticaDentistry.Core.Exceptions;

namespace DenticaDentistry.Core.ValueObjects;

public sealed record DentistId
{
    public Guid Value { get; }

    public DentistId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }
    
    public static implicit operator Guid(DentistId date) => date.Value;

    public static implicit operator DentistId(Guid value) => new(value);
}
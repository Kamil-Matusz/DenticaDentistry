namespace Dentica_Dentistry.Application.Services;

public class Clock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}
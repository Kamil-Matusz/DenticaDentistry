namespace DenticaDentistry.Core.Exceptions;

public class InvalidPhoneNumberException : CustomException
{
    public string PhoneNumber { get; }
    
    public InvalidPhoneNumberException(string phoneNumber) : base($"PhoneNumber: '{phoneNumber}' is invalid.")
    {
        PhoneNumber = phoneNumber;
    }
}
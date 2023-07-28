namespace DenticaDentistry.Application.DTO;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Fullname { get; set; }
    public string PhoneNumber { get; set; }
}
using DenticaDentistry.Core.ValueObjects;

namespace DenticaDentistry.Core.Entities;

public class User
{
    public UserId UserId { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Fullname Fullname { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Role Role { get; set; }

    protected User()
    {
        
    }
    public User(UserId userId, Email email, Username username, Password password, Fullname fullName, Role role, PhoneNumber phoneNumber)
    {
        UserId = userId;
        Email = email;
        Username = username;
        Password = password;
        Fullname = fullName;
        PhoneNumber = phoneNumber;
        Role = role;
    }
    
    public void ChangePassword(Password newPassword)
    {
        Password = newPassword;
    }
}
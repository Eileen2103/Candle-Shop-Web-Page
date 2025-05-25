namespace backend.Models;

public class User
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? password { get; set; } = default!;
    public string? Address { get; set; }
    public string PhoneNumber { get; set; }

    public User()
    {
        Name = string.Empty;
        Email = string.Empty;
        password = string.Empty;
        PhoneNumber = string.Empty;
    }

    public User(string name, string email, string password, string phoneNumber)
    {
        Name = name;
        Email = email;
        this.password = password;
        PhoneNumber = phoneNumber;
    }

}
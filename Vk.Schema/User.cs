namespace Vk.Schema;

public class UserCreateRequest
{
    public int UserNumber { get; set; }        // User Numarasi
    public string FirstName { get; set; }      // User Adi
    public string LastName { get; set; }       // User Soyadi
    public string Email { get; set; }          // User Email
    public string Password { get; set; }       // User Password
    public string Role { get; set; }           // User Rol
}

public class UserUpdateRequest
{
    public string FirstName { get; set; }      // User Adi
    public string LastName { get; set; }       // User Soyadi
    public string Email { get; set; }          // User Email
    public string Password { get; set; }       // User Password
}

public class UserResponse
{
    public int UserNumber { get; set; }        // User Numarasi
    public string FullUserName { get; set; }   // User Name + Surname
    public string Email { get; set; }          // User Email
    public string Role { get; set; }           // User Rol
    
}
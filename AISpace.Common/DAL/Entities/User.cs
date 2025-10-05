namespace AISpace.Common.DAL.Entities;

public class User
{
    public int Id { get; set; }           // Primary key
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public ICollection<Character> Characters { get; set; } = [];
}

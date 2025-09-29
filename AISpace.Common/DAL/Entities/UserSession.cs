namespace AISpace.Common.DAL.Entities;

public class UserSession
{
    public int Id { get; set; }           // Primary key
    public int UserID { get; set; }
    public User User { get; set; } = null!;
    public string OTP { get; set; } = string.Empty;
}

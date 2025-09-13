namespace AISpace.Common.DAL.Entities;

public class UserSession
{
    public int Id { get; set; }           // Primary key
    public uint UserID { get; set; }
    public string OTP { get; set; } = string.Empty;
}

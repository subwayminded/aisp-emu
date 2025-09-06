namespace AISpace.Common.DAL;

public class UserSession
{
    public int Id { get; set; }           // Primary key
    public string GUID { get; set; } = string.Empty;
    public string UserID { get; set; } = string.Empty;
    public string OTP { get; set; } = string.Empty;
}

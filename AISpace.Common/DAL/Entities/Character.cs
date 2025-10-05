namespace AISpace.Common.DAL.Entities;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int UserId { get; set; }

    public User User { get; set; }
    public ICollection<Equipment> Equips { get; set; } = [];
}

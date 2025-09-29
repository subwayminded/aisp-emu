namespace AISpace.Common.DAL.Entities;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Equipment> Equips { get; set; } = [];
}

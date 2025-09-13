

namespace AISpace.Common.DAL.Entities;

public class Chara
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Equip> Equips { get; set; } = [];
}

public class Equip
{
    public int Id { get; set; }
    public string ItemName { get; set; } = string.Empty;

    // Foreign key
    public int ChararId { get; set; }

    // Navigation back to Character
    public Chara Chara { get; set; } = null!;
}

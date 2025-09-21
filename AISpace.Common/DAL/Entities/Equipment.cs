

namespace AISpace.Common.DAL.Entities;

public class Equipment
{
    public int Id { get; set; }
    public string EquipmentName { get; set; } = string.Empty;

    // Foreign key
    public int CharaId { get; set; }

    // Navigation back to Character
    public Character Chara { get; set; } = null!;
}

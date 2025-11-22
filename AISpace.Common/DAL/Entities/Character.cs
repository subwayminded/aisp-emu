using System.Reflection;
using AISpace.Common.Game;

namespace AISpace.Common.DAL.Entities;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public uint ModelId { get; set; }
    public BloodType BloodType { get; set; }

    public DateTime Birthdate { get; set; }
    public int Gender { get; set; }
    public uint FaceType { get; set; }
    public uint Hairstyle { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public ICollection<CharacterInventory> Inventory { get; set; } = new List<CharacterInventory>();
    public ICollection<CharacterEquipment> Equipment { get; set; } = new List<CharacterEquipment>();
}

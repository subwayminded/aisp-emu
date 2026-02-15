using AISpace.Common.DAL.Repositories;
using AISpace.Common.Game;
using AISpace.Common.Network.Packets.Area;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreaAvatarGetDataHandler(ILogger<AreaAvatarGetDataHandler> logger, ICharacterRepository charRepo) : IPacketHandler
{
    public PacketType RequestType => PacketType.AvatarGetDataRequest;

    public PacketType ResponseType => PacketType.AvatarNotifyData;

    public MessageDomain Domain => MessageDomain.Area;

    private readonly ILogger<AreaAvatarGetDataHandler> _logger = logger;
    private readonly ICharacterRepository _charRepo = charRepo;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        if (!connection.IsAuthenticated || connection.User == null || connection.User.Characters.First() == null)
            return;
        _logger.LogInformation("Received AvatarGetDataRequest from Client: {Id}, IsAuthed: {auth}", connection.Id, connection.IsAuthenticated);
        _logger.LogInformation("Received AvatarGetDataRequest from Client: {Id}", connection.Id);

        var cha = connection.User!.Characters.First();
        if (cha == null)
            return;
        _logger.LogInformation("Processing AvatarGetDataRequest for Character: {CharacterName} (ID: {CharacterId})", cha.Name, cha.Id);
        var charaData = new CharaData(cha.ModelId, (uint)cha.Id, cha.Name);
        charaData.Visual.VisualId = (uint)cha.Id;
        charaData.Visual.BloodType = cha.BloodType;
        charaData.Visual.Month = (byte)cha.Birthdate.Month;
        charaData.Visual.Day = (byte)cha.Birthdate.Day;
        charaData.Visual.Gender = (uint)cha.Gender;
        charaData.Visual.Face = (byte)cha.FaceType;
        charaData.Visual.Hairstyle = cha.Hairstyle;
        // Fill 30 slots by SlotIndex so client gets correct slot mapping
        for (byte slot = 0; slot < 30; slot++)
        {
            var eq = cha.Equipment.FirstOrDefault(e => e.SlotIndex == slot);
            charaData.AddEquip(eq != null ? (uint)eq.ItemId : 10100220, slot);
        }
        var avatarData = new AvatarData(0, charaData);
        var notifyData = new AvatarNotifyData(0, avatarData);
        await connection.SendAsync(ResponseType, notifyData.ToBytes(), ct);
    }
}

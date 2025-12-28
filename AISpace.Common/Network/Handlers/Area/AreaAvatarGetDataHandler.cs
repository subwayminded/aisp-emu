using AISpace.Common.DAL.Repositories;
using AISpace.Common.Game;
using AISpace.Common.Network.Packets.Area;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreaAvatarGetDataHandler(ILogger<AreaAvatarGetDataHandler> logger, ICharacterRepository charRepo) : IPacketHandler
{
    public PacketType RequestType => PacketType.AvatarGetDataRequest;

    public PacketType ResponseType => PacketType.AvatarNotifyData;

    public MessageDomain Domains => MessageDomain.Area;

    private readonly ILogger<AreaAvatarGetDataHandler> _logger = logger;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        if (!connection.IsAuthenticated)
            return;
        _logger.LogInformation("Received AvatarGetDataRequest from Client: {Id}, IsAuthed: {auth}", connection.Id, connection.IsAuthenticated);
        _logger.LogInformation("Received AvatarGetDataRequest from Client: {Id}", connection.Id);

        var cha = connection.clientUser?.Characters.First();
        _logger.LogInformation("Processing AvatarGetDataRequest for Character: {CharacterName} (ID: {CharacterId})", cha?.Name, cha?.Id);
        //TODO: Figure out ID's. Maybe merge CharacterData with AvatarData/CharaData?
        var charaData = new CharaData(0, 0, cha.Name);
        charaData.Visual.CharacterID = 0;
        charaData.Visual.BloodType = cha.BloodType;
        charaData.Visual.Month = (byte)cha.Birthdate.Month;
        charaData.Visual.Day = (byte)cha.Birthdate.Day;
        charaData.Visual.Gender = (uint)cha.Gender;
        charaData.Visual.Face = (byte)cha.FaceType;
        charaData.Visual.Hairstyle = cha.Hairstyle;
        foreach (var item in cha.Equipment)
        {
            _logger.LogInformation("Client: {ClientId} requested AvatarGetData. Adding Equipment {equp}", connection.Id, item.ItemId);
            charaData.AddEquip((uint)item.ItemId, 0);
        }
        //charaData.AddEquip(10100140, 0);
        //charaData.AddEquip(10200130, 0);
        //charaData.AddEquip(10100190, 0);
        //charaData.AddEquip(10100180, 0);
        var avatarData = new AvatarData(0, charaData);
        var notifyData = new AvatarNotifyData(0, avatarData);
        await connection.SendAsync(ResponseType, notifyData.ToBytes(), ct);
    }
}

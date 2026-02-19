using AISpace.Common.DAL.Repositories;
using AISpace.Common.Game;
using AISpace.Common.Network.Packets.Area;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreaAvatarGetDataHandler(ILogger<AreaAvatarGetDataHandler> logger) : IPacketHandler
{
    public PacketType RequestType => PacketType.AvatarGetDataRequest;
    public PacketType ResponseType => PacketType.AvatarNotifyData;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        if (connection.User == null) return;
        var cha = connection.User.Characters.FirstOrDefault();
        if (cha == null) return;

        logger.LogInformation($"[DATA] Sending self-data for {cha.Name}");

        // Result 0 = Это ТЫ. Обязательно для управления.
        var myPos = new MovementData(connection.X, connection.Y, connection.Z, connection.Rotation, MovementType.Stopped);
        var notifyData = new AvatarNotifyData(0, new AvatarData(0, CreateCData(cha, myPos)));

        await connection.SendAsync(ResponseType, notifyData.ToBytes(), ct);
    }

    private static CharaData CreateCData(DAL.Entities.Character cha, MovementData pos) {
        var cd = new CharaData((uint)cha.Id, (uint)cha.Id, cha.Name) { moveData = pos };
        cd.Visual.VisualId = (uint)cha.Id;
        cd.Visual.BloodType = cha.BloodType;
        cd.Visual.Month = (byte)cha.Birthdate.Month;
        cd.Visual.Day = (byte)cha.Birthdate.Day;
        cd.Visual.Gender = (uint)cha.Gender;
        cd.Visual.Face = (byte)cha.FaceType;
        cd.Visual.Hairstyle = cha.Hairstyle;
        for (byte s = 0; s < 30; s++) {
            var eq = cha.Equipment.FirstOrDefault(e => e.SlotIndex == s);
            cd.AddEquip(eq != null ? (uint)eq.ItemId : 0, s);
        }
        return cd;
    }
}
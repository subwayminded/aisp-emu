using AISpace.Common.DAL.Entities;
using AISpace.Common.DAL.Repositories;
using AISpace.Common.Game;
using AISpace.Common.Network.Packets.Area;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreasvEnterHandler(IUserSessionRepository sessionRepo, ILogger<AreasvEnterHandler> logger, SharedState sharedState) : IPacketHandler
{
    public PacketType RequestType => PacketType.AreasvEnterRequest;
    public PacketType ResponseType => PacketType.AreasvEnterResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var req = AreasvEnterRequest.FromBytes(payload.Span);
        var session = await sessionRepo.GetValidSessionAsync(req.OTP, ct);
        if (session is null) return;

        connection.User = session.User;
        var cha = connection.User.Characters.FirstOrDefault();
        if (cha == null) return;

        connection.CharacterId = (uint)cha.Id;
        await connection.SendAsync(ResponseType, new AreasvEnterResponse(0, connection.CharacterId).ToBytes(), ct);

        _ = Task.Run(async () => {
            await Task.Delay(1500, ct);
            var myPos = new MovementData(connection.X, connection.Y, connection.Z, connection.Rotation, MovementType.Stopped);
            var myData = CreateNotify(cha, 1, myPos).ToBytes();

            foreach (var other in sharedState.AreaClients.Values) {
                if (other.Id == connection.Id) continue;
                await other.SendAsync(PacketType.AvatarNotifyData, myData, ct);
                var oCha = other.User?.Characters.FirstOrDefault();
                if (oCha != null) {
                    var oPos = new MovementData(other.X, other.Y, other.Z, other.Rotation, MovementType.Stopped);
                    await connection.SendAsync(PacketType.AvatarNotifyData, CreateNotify(oCha, 1, oPos).ToBytes(), ct);
                }
            }
            await connection.SendAsync(PacketType.AvatarNotifyData, CreateNotify(cha, 0, myPos).ToBytes(), ct);
        });
    }

    private static AvatarNotifyData CreateNotify(Character cha, uint res, MovementData pos) {
        var cData = new CharaData((uint)cha.Id, cha.ModelId, cha.Name) { moveData = pos };
        cData.Visual.VisualId = (uint)cha.Id;
        cData.Visual.BloodType = cha.BloodType;
        cData.Visual.Month = (byte)cha.Birthdate.Month;
        cData.Visual.Day = (byte)cha.Birthdate.Day;
        cData.Visual.Gender = (uint)cha.Gender;
        cData.Visual.Face = (byte)cha.FaceType;
        cData.Visual.Hairstyle = cha.Hairstyle;
        for (byte s = 0; s < 30; s++) {
            var eq = cha.Equipment.FirstOrDefault(e => e.SlotIndex == s);
            cData.AddEquip(eq != null ? (uint)eq.ItemId : 0, s);
        }
        return new AvatarNotifyData(res, new AvatarData(res, cData));
    }
}
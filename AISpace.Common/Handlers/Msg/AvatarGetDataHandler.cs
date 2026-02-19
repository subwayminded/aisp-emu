using AISpace.Common.DAL.Entities;
using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Msg;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Msg;

public class AvatarGetDataHandler(ILogger<AvatarGetDataHandler> logger) : IPacketHandler
{
    public PacketType RequestType => PacketType.AvatarGetDataRequest;
    public PacketType ResponseType => PacketType.AvatarDataResponse;
    public MessageDomain Domain => MessageDomain.Msg;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        if (!connection.IsAuthenticated || connection.User == null) return;

        // Берем персонажа из уже загруженного User в соединении
        var cha = connection.User.Characters.FirstOrDefault();

        if (cha != null)
        {
            logger.LogInformation($"[DB] Found character '{cha.Name}' for {connection.User.Username}");
            
            var resp = new AvatarDataResponse(0, cha.Name, cha.ModelId, 0, 0);
            resp.Visual.VisualId = (uint)cha.Id;
            resp.Visual.BloodType = cha.BloodType;
            resp.Visual.Month = (byte)cha.Birthdate.Month;
            resp.Visual.Day = (byte)cha.Birthdate.Day;
            resp.Visual.Gender = (uint)cha.Gender;
            resp.Visual.Face = (byte)cha.FaceType;
            resp.Visual.Hairstyle = cha.Hairstyle;

            for (byte slot = 0; slot < 30; slot++) {
                var eq = cha.Equipment.FirstOrDefault(e => e.SlotIndex == slot);
                resp.AddEquip(eq != null ? (uint)eq.ItemId : 0, slot);
            }
            await connection.SendAsync(ResponseType, resp.ToBytes(), ct);
        }
        
        await connection.SendAsync(PacketType.AvatarGetDataResponse, new AvatarGetDataResponse(0).ToBytes(), ct);
    }
}
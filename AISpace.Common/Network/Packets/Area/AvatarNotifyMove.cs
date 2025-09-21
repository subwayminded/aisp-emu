using AISpace.Common.Game;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace AISpace.Common.Network.Packets.Area;

public class AvatarNotifyMove(uint avatar_Id, MovementData moveData) : IPacket<AvatarNotifyMove>
{
    public static AvatarNotifyMove FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)1);
        writer.Write(avatar_Id);
        writer.Write(moveData.ToBytes());
        return writer.ToBytes();
    }
}

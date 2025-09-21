using AISpace.Common.Game;

namespace AISpace.Common.Network.Packets.Area;

public class AvatarNotifyData(uint result, AvatarData avatarData) : IPacket<AvatarNotifyData>
{
    public static AvatarNotifyData FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(result);
        writer.Write(avatarData.ToBytes());
        return writer.ToBytes();
    }
}

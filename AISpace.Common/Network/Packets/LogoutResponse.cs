
namespace AISpace.Common.Network.Packets;

internal class LogoutResponse : IPacket<LogoutResponse>
{
    public static LogoutResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(0);
        return writer.ToBytes();
    }
}

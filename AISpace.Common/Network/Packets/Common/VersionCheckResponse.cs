namespace AISpace.Common.Network.Packets.Common;

public class VersionCheckResponse(uint Result, uint Major, uint Minor, uint Ver) : IPacket<VersionCheckResponse>
{
    public static VersionCheckResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(Result, Major, Minor, Ver);
        return writer.ToBytes();
    }
}

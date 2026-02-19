using AISpace.Common.Network;

namespace AISpace.Common.Network.Packets.Area;

public class GetMyAvatarMyprofileDataResponse(uint result, string text = "") : IPacket<GetMyAvatarMyprofileDataResponse>
{
    private const int BufferSize = 1280;

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)0); // Result = Success

        // Просто 1280 нулей. Это самый безопасный вариант.
        byte[] zeros = new byte[BufferSize];
        writer.Write(zeros);

        return writer.ToBytes();
    }

    public static GetMyAvatarMyprofileDataResponse FromBytes(ReadOnlySpan<byte> data) => throw new NotImplementedException();
}
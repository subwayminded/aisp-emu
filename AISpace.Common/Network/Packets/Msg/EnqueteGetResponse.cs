
using AISpace.Common.Game;

namespace AISpace.Common.Network.Packets.Msg;

public class EnqueteGetResponse(uint result, List<EnqueteData> quiz) : IPacket<EnqueteGetResponse>
{
    public static EnqueteGetResponse FromBytes(ReadOnlySpan<byte> data) => throw new NotImplementedException();

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(result);
        writer.Write((uint)quiz.Count);
        foreach (var item in quiz)
            writer.Write(item.ToBytes());
        return writer.ToBytes();
    }
}

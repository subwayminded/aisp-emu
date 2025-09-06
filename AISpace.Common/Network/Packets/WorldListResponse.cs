using AISpace.Common.Game;

namespace AISpace.Common.Network.Packets;

public class WorldListResponse(List<WorldEntry> worlds) : IPacket<WorldListResponse>
{
    List<WorldEntry> Worlds = worlds;
    uint _length = 881;

    public static WorldListResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[(int)(_length + 5)];
        var writer = new PacketWriter(buffer);
        writer.WriteByte(0x03);
        writer.WriteUInt32LE(_length);
        writer.WriteUInt16LE((ushort)PacketType.WorldListResponse);//Packet Type

        writer.WriteUInt32LE(0);//Result
        writer.WriteUInt32LE(1);//Count of worlds?
        writer.WriteUInt32LE(Worlds[0].ID);//World ID
        writer.WriteFixedAsciiString(Worlds[0].Name, 97); //World Name
        writer.WriteFixedAsciiString(Worlds[0].Description, 766); //World Description
        writer.WriteUInt32LE(0); //Padding
        return writer.WrittenBytes;
    }
}

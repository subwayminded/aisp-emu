using AISpace.Common.DAL.Entities;

namespace AISpace.Common.Network.Packets.Auth;

public class WorldListResponse(uint result, List<DAL.Entities.World> worlds) : IPacket<WorldListResponse>
{

    public static WorldListResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(result);//Result
        writer.Write((uint)worlds.Count);//Result
        foreach (var world in worlds)
        {
            writer.Write((uint)world.Id);//Result
            writer.WriteFixedAsciiString(world.Name, 97); //World Name
            writer.WriteFixedAsciiString(world.Description, 766); //World Description
        }
        writer.Write((uint)0); //Padding
        return writer.ToBytes();
    }
}

namespace AISpace.Common.Network.Packets.Auth;

public class WorldListResponse(uint Result, List<DAL.Entities.World> Worlds) : IPacket<WorldListResponse>
{

    public static WorldListResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(Result);
        writer.Write((uint)Worlds.Count);
        foreach (var world in Worlds)
        {
            writer.Write((uint)world.Id);
            writer.WriteFixedString(world.Name, 97, "ASCII"); //World Name
            writer.WriteFixedString(world.Description, 766, "ASCII"); //World Description
        }
        writer.Write((uint)0); //Padding?
        return writer.ToBytes();
    }
}

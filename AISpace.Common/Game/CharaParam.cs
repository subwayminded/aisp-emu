using AISpace.Common.Network;

namespace AISpace.Common.Game;

public class CharaParam()
{

    public byte[] ToBytes()
    {
        using PacketWriter writer = new();
        writer.Write(new byte[37]);
        return writer.ToBytes();
    }
}

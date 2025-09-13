using System.Security.Cryptography;
using System.Text;

namespace AISpace.Common.Network.Packets.World;

public class WorldSelectResponse(uint result, string ipAddress, ushort port, string otp) : IPacket<WorldSelectResponse>
{
    uint count=1;
    public string ipAddress = ipAddress;
    public ushort port = port;
    public string otp = otp;

    public static WorldSelectResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(result, count);//Result and World count
        writer.Write(port);
        writer.WriteFixedAsciiString(ipAddress, 65);
        writer.WriteFixedAsciiString(otp, 20);
        return writer.ToBytes();
    }
}

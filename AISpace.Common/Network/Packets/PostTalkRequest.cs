
namespace AISpace.Common.Network.Packets;

public class PostTalkRequest(uint messageID, uint distID, string message, uint balloonID) : IPacket<PostTalkRequest>
{
    public uint MessageID = messageID;
    public uint DistID = distID;
    public string Message = message;
    public uint BalloonID = balloonID;
    //BalloonID is either normal talk or Shout

    public static PostTalkRequest FromBytes(ReadOnlySpan<byte> data)
    {
        var reader = new PacketReader(data);
        uint msgId = reader.ReadUInt32LE();
        uint distId = reader.ReadUInt32LE();
        string messsage = reader.ReadNullTerminatedAscii();
        uint balloonId = reader.ReadUInt32LE();
        var temp = new PostTalkRequest(msgId, distId, messsage, balloonId);
        return temp;
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(MessageID);
        writer.Write(DistID);
        writer.WriteNullTerminatedAsciiString(Message);
        writer.Write(BalloonID);
        return writer.ToBytes();
    }
}

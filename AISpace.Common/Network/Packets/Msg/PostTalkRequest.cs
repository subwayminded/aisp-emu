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
        uint msgId = reader.ReadUInt();
        uint distId = reader.ReadUInt();
        string messsage = reader.ReadString();
        uint balloonId = reader.ReadUInt();
        var temp = new PostTalkRequest(msgId, distId, messsage, balloonId);
        return temp;
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(MessageID);
        writer.Write(DistID);
        writer.Write(Message);
        writer.Write(BalloonID);
        return writer.ToBytes();
    }
}

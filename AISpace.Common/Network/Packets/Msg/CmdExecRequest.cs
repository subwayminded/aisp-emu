namespace AISpace.Common.Network.Packets.Msg;

public class CmdExecRequest(uint messageId, string command, uint argCount, List<string>? arguments = null) : IPacket<CmdExecRequest>
{
    const int MaxArgs= 10;
    const int ArgLength = 384;
    const int CmdLength = 96;

    public uint MessageId = messageId;
    public string Command = command;
    public uint ArgCount = argCount;

    public List<string> Arguments { get; } = arguments ?? [];

    public const int FullPayloadLength = 4 + 96 + (10 * 0x180) + 4;

    public static CmdExecRequest FromBytes(ReadOnlySpan<byte> data)
    {
        var reader = new PacketReader(data);

        var msgId = reader.ReadUInt();
        var cmd = reader.ReadFixedString(CmdLength, "ASCII");

        var args = new List<string>();
        for (int i = 0; i < MaxArgs; i++)
        {
            string arg = reader.ReadFixedString(ArgLength, "ASCII");
            args.Add(arg);
        }
        uint argCount = reader.ReadUInt();

        return new CmdExecRequest(msgId, cmd, argCount, [.. args.Take((int)argCount)]);
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}

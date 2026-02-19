namespace AISpace.Common.Network.Packets.Msg;

public class ChannelSelectResponse(uint result, Game.ServerInfo serverInfo, uint mapId, uint mapSerialId) : IPacket<ChannelSelectResponse>
{
    public uint Result = result;
    public Game.ServerInfo serverInfo = serverInfo;
    public uint MapID = mapId;
    public uint MapSerialID = mapSerialId;

    public static ChannelSelectResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(Result);
        writer.Write(serverInfo.ToBytes());
        writer.Write(MapID);
        writer.Write(MapSerialID);
        return writer.ToBytes();
    }
}

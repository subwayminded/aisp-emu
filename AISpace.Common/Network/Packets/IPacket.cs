namespace AISpace.Common.Network.Packets;


public interface IPacket<T> where T : IPacket<T>
{
    byte[] ToBytes();
    static abstract T FromBytes(ReadOnlySpan<byte> data);
}

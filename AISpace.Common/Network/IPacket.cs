namespace AISpace.Common.Network;


public interface IPacket<T> where T : IPacket<T>
{
    byte[] ToBytes();
    static abstract T FromBytes(ReadOnlySpan<byte> data);
}

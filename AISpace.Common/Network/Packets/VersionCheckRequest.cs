using System.Buffers.Binary;

namespace AISpace.Common.Network.Packets
{
    public class VersionCheckRequest(uint Major, uint Minor, uint Version) : IPacket<VersionCheckRequest>
    {
        public uint Major = Major;
        public uint Minor = Minor;
        public uint Version = Version;

        public static VersionCheckRequest FromBytes(ReadOnlySpan<byte> data)
        {
            uint major = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(0, 4));
            uint minor = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(4, 4));
            uint version = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(8, 4));
            return new VersionCheckRequest(major, minor, version);
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}

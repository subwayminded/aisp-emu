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
            PacketReader reader = new(data);
            uint major = reader.ReadUInt32LE();
            uint minor = reader.ReadUInt32LE();
            uint version = reader.ReadUInt32LE();
            return new VersionCheckRequest(major, minor, version);
        }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}

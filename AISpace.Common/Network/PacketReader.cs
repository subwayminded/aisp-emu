using System.Buffers.Binary;
using System.Text;

namespace AISpace.Common.Network;

public ref struct PacketReader
{
    private readonly ReadOnlySpan<byte> _buffer;
    private int _offset;

    public PacketReader(ReadOnlySpan<byte> buffer)
    {
        _buffer = buffer;
        _offset = 0;
    }

    public readonly int Position => _offset;
    public readonly bool EndOfSpan => _offset >= _buffer.Length;

    public byte ReadByte()
    {
        return _buffer[_offset++];
    }

    public ushort ReadUInt16LE()
    {
        ushort value = BinaryPrimitives.ReadUInt16LittleEndian(_buffer.Slice(_offset, 2));
        _offset += 2;
        return value;
    }

    public uint ReadUInt32LE()
    {
        uint value = BinaryPrimitives.ReadUInt32LittleEndian(_buffer.Slice(_offset, 4));
        _offset += 4;
        return value;
    }

    public ReadOnlySpan<byte> ReadBytes(int count)
    {
        var slice = _buffer.Slice(_offset, count);
        _offset += count;
        return slice;
    }

    public string ReadUtf8String(int length)
    {
        var slice = ReadBytes(length);
        return Encoding.UTF8.GetString(slice);
    }

    public string ReadNullTerminatedAscii()
    {
        int start = _offset;
        int end = _offset;

        // Find the null terminator
        while (end < _buffer.Length && _buffer[end] != 0)
            end++;

        // Slice from start to end (exclusive of null)
        var slice = _buffer.Slice(start, end - start);

        // Advance past the string + null terminator (if found)
        _offset = end < _buffer.Length ? end + 1 : end;

        return Encoding.ASCII.GetString(slice);
    }
}
using System.Buffers.Binary;
using System.Drawing;
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

    public sbyte ReadSByte()
    {
        return (sbyte)_buffer[_offset++];
    }

    public float ReadFloat()
    {
        int size = sizeof(float);
        float value = BinaryPrimitives.ReadSingleLittleEndian(_buffer.Slice(_offset, size));
        _offset += size;
        return value;
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
        int end = _offset;

        while (end < _buffer.Length && _buffer[end] != 0)
            end++;
        var slice = _buffer[_offset..end];
        _offset = end < _buffer.Length ? end + 1 : end;

        return Encoding.ASCII.GetString(slice);
    }
}
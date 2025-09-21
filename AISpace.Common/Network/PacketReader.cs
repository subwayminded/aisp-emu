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
        float value = BinaryPrimitives.ReadSingleLittleEndian(_buffer.Slice(_offset, 4));
        _offset += 4;
        return value;
    }
    public ushort ReadUShort()
    {
        ushort value = BinaryPrimitives.ReadUInt16LittleEndian(_buffer.Slice(_offset, 2));
        _offset += 2;
        return value;
    }

    public uint ReadUInt()
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

    public string ReadFixedString(int length, string encoderName = "UTF8")
    {
        var encoder = Encoding.GetEncoding(encoderName);
        var slice = ReadBytes(length);
        return encoder.GetString(slice);
    }

    public string ReadString(string encoderName = "ASCII")
    {
        var encoder = Encoding.GetEncoding(encoderName);
        var slice = _buffer[_offset..];
        int end = slice.IndexOf((byte)0x00);
        if (end < 0) 
            end = slice.Length;

        _offset += end < slice.Length ? end + 1 : end;

        return encoder.GetString(slice[..end]);
    }
}
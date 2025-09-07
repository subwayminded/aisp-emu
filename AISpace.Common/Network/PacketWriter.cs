using System.Buffers.Binary;
using System.Text;

namespace AISpace.Common.Network;

public ref struct PacketWriter
{
    private Span<byte> _buffer;
    private int _offset;
    private readonly Encoding shiftJis = Encoding.GetEncoding("Shift_JIS");
    public PacketWriter(Span<byte> buffer)
    {
        _buffer = buffer;
        _offset = 0;
    }

    public readonly int Position => _offset;
    public readonly int Length => _offset;
    public readonly Span<byte> Written => _buffer[.._offset];
    public readonly byte[] WrittenBytes => Written.ToArray();

    public void WriteByte(byte value)
    {
        _buffer[_offset++] = value;
    }
    public void WriteBytes(byte[] value)
    {
        value.CopyTo(_buffer.Slice(_offset, value.Length));
        _offset += value.Length;
    }

    public void WriteUInt16LE(ushort value)
    {
        BinaryPrimitives.WriteUInt16LittleEndian(_buffer.Slice(_offset, 2), value);
        _offset += 2;
    }

    public void WriteUInt32LE(uint value)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(_buffer.Slice(_offset, 4), value);
        _offset += 4;
    }

    public void WriteBytes(ReadOnlySpan<byte> source)
    {
        source.CopyTo(_buffer[_offset..]);
        _offset += source.Length;
    }

    public void WriteFixedJisString(string value, int length)
    {
        Span<byte> encoded = stackalloc byte[length]; // temporary space
        int written = Encoding.ASCII.GetBytes(value, encoded);

        // Clamp length (truncate if too long)
        if (written > length) written = length;

        // Copy the string part
        encoded[..written].CopyTo(_buffer.Slice(_offset, length));

        // Fill remaining space with nulls
        if (written < length) _buffer.Slice(_offset + written, length - written).Clear();

        _offset += length;
    }

    public void WriteNullTerminatedAsciiString(string value, int maxLength = 100)
    {
        // Reserve space for string + null
        var dest = _buffer.Slice(_offset, maxLength);

        // Encode string directly into destination
        int written = Encoding.ASCII.GetBytes(value, dest);

        // Clamp so we always leave room for the null
        if (written >= maxLength)
            written = maxLength - 1;

        // Ensure null terminator
        dest[written] = 0;

        // Advance past string + null
        _offset += written + 1;
    }

    public void WriteFixedAsciiString(string value, int length)
    {
        // Encode string as ASCII
        Span<byte> encoded = stackalloc byte[length]; // temporary space
        int written = Encoding.ASCII.GetBytes(value, encoded);

        // Clamp length (truncate if too long)
        if (written > length) written = length;

        // Copy the string part
        encoded[..written].CopyTo(_buffer.Slice(_offset, length));

        // Fill remaining space with nulls
        if (written < length) _buffer.Slice(_offset + written, length - written).Clear();

        _offset += length;
    }
}

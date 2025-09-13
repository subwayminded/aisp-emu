using System.Buffers;
using System.Buffers.Binary;
using System.Text;

namespace AISpace.Common.Network;

public ref struct PacketWriter : IDisposable
{
    private Span<byte> _buffer;
    private byte[]? _pooled;
    private int _offset;

    private readonly Encoding shiftJis = Encoding.GetEncoding("Shift_JIS");

    public PacketWriter()
    {
        _buffer = new byte[512];
        _pooled = null;
        _offset = 0;
    }

    public readonly int Position => _offset;
    public readonly int Length => _offset;
    public readonly ReadOnlySpan<byte> Written => _buffer[.._offset];
    public readonly byte[] ToBytes() => Written.ToArray();


    private void EnsureCapacity(int needed)
    {
        if (_offset + needed <= _buffer.Length)
            return;

        int newSize = _offset + needed;
        byte[] newBuf = ArrayPool<byte>.Shared.Rent(newSize);
        _buffer[.._offset].CopyTo(newBuf);

        if (_pooled != null)
            ArrayPool<byte>.Shared.Return(_pooled);

        _pooled = newBuf;
        _buffer = newBuf;
    }

    public void Write(ushort value)
    {
        var size = sizeof(ushort);
        EnsureCapacity(size);
        BinaryPrimitives.WriteUInt16LittleEndian(_buffer.Slice(_offset, size), value);
        _offset += size;
    }
    public void Write(params ulong[] values)
    {
        foreach (var value in values)
            Write(value);
    }
    public void Write(ulong value)
    {
        var size = sizeof(ulong);
        EnsureCapacity(size);
        BinaryPrimitives.WriteUInt64LittleEndian(_buffer.Slice(_offset, size), value);
        _offset += size;
    }
    public void Write(params ushort[] values)
    {
        foreach (var value in values)
            Write(value);
    }
    public void Write(uint value)
    {
        var size = sizeof(uint);
        EnsureCapacity(size);
        BinaryPrimitives.WriteUInt32LittleEndian(_buffer.Slice(_offset, size), value);
        _offset += size;
    }

    public void Write(params uint[] values)
    {
        foreach (var value in values)
            Write(value);
    }
    public void Write(int value)
    {
        var size = sizeof(int);
        EnsureCapacity(size);
        BinaryPrimitives.WriteInt32LittleEndian(_buffer.Slice(_offset, size), value);
        _offset += size;
    }

    public void Write(params int[] values)
    {
        foreach (var value in values)
            Write(value);
    }
    public void Write(short value)
    {
        var size = sizeof(short);
        EnsureCapacity(size);
        BinaryPrimitives.WriteInt16LittleEndian(_buffer.Slice(_offset, size), value);
        _offset += size;
    }

    public void Write(params short[] values)
    {
        foreach (var value in values)
            Write(value);
    }
    public void Write(float value)
    {
        var size = sizeof(float);
        EnsureCapacity(size);
        BinaryPrimitives.WriteSingleLittleEndian(_buffer.Slice(_offset, size), value);
        _offset += size;
    }
    public void Write(params float[] values)
    {
        foreach (var value in values)
            Write(value);
    }
    public void Write(byte value)
    {
        var size = sizeof(byte);
        EnsureCapacity(size);
        _buffer[_offset++] = value;
    }
    public void Write(sbyte value)
    {
        var size = sizeof(sbyte);
        EnsureCapacity(size);
        _buffer[_offset++] = (byte)value;
    }
    public void Write(ReadOnlySpan<byte> source)
    {
        var size = source.Length;
        EnsureCapacity(size);
        source.CopyTo(_buffer[_offset..]);
        _offset += size;
    }

    public void Write(string value, byte terminator = 0x00, Encoding? encoder = null)
    {
        if (encoder == null)
            encoder = Encoding.ASCII;
        int size = encoder.GetByteCount(value);
        EnsureCapacity(size + 1); // +1 for terminator
        encoder.GetBytes(value, _buffer[_offset..]);
        _offset += size;
        _buffer[_offset++] = terminator;
    }




    //Old
    public void WriteByte(byte value)
    {
        EnsureCapacity(1);
        _buffer[_offset++] = value;
    }
    public void WriteSByte(sbyte value)
    {
        EnsureCapacity(1);
        _buffer[_offset++] = (byte)value;
    }
    public void WriteBytes(byte[] value)
    {
        EnsureCapacity(value.Length);
        value.CopyTo(_buffer.Slice(_offset, value.Length));
        _offset += value.Length;
    }

    public void WriteUShortLE(ushort value)
    {
        EnsureCapacity(2);
        BinaryPrimitives.WriteUInt16LittleEndian(_buffer.Slice(_offset, 2), value);
        _offset += 2;
    }

    public void WriteFloatLE(float value)
    {
        EnsureCapacity(4);
        BinaryPrimitives.WriteSingleLittleEndian(_buffer.Slice(_offset, 4), value);
        _offset += 4;
    }

    public void WriteUIntLE(uint value)
    {
        EnsureCapacity(4);
        BinaryPrimitives.WriteUInt32LittleEndian(_buffer.Slice(_offset, 4), value);
        _offset += 4;
    }

    public void WriteUIntLE(params uint[] values)
    {
        foreach (var value in values)
            WriteUIntLE(value);
    }

    public void WriteBytes(ReadOnlySpan<byte> source)
    {
        EnsureCapacity(source.Length);
        source.CopyTo(_buffer[_offset..]);
        _offset += source.Length;
    }

    public void WriteFixedJisString(string value, int length)
    {
        
        Span<byte> encoded = stackalloc byte[length]; // temporary space
        //TODO: Fix this to use Shift_JIS
        int written = Encoding.ASCII.GetBytes(value, encoded);

        // Clamp length (truncate if too long)
        if (written > length) written = length;

        EnsureCapacity(length);
        // Copy the string part
        encoded[..written].CopyTo(_buffer.Slice(_offset, length));

        // Fill remaining space with nulls
        if (written < length) _buffer.Slice(_offset + written, length - written).Clear();

        _offset += length;
    }

    public void WriteNullTerminatedAsciiString(string value, int maxLength = 100)
    {
        int length = Math.Min(value.Length + 1, maxLength); // +1 for null terminator
        EnsureCapacity(length);
        // Reserve space for string + null
        var dest = _buffer.Slice(_offset, length);

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
        EnsureCapacity(length);
        // Clamp length (truncate if too long)
        if (written > length) written = length;

        // Copy the string part
        encoded[..written].CopyTo(_buffer.Slice(_offset, length));

        // Fill remaining space with nulls
        if (written < length) _buffer.Slice(_offset + written, length - written).Clear();

        _offset += length;
    }

    public void Dispose()
    {
    }
}

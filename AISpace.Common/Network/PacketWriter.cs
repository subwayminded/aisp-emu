using System.Buffers.Binary;
using System.Text;

namespace AISpace.Common.Network;

public class PacketWriter : IDisposable
{
    private readonly MemoryStream _stream = new();

    public long Position => _stream.Position;
    public long Length => _stream.Length;
    public byte[] ToBytes() => _stream.ToArray();

    public void Dispose() => _stream.Dispose();

    public void Write(ushort value)
    {
        Span<byte> buffer = stackalloc byte[2];
        BinaryPrimitives.WriteUInt16LittleEndian(buffer, value);
        _stream.Write(buffer);
    }
    public void Write(short value)
    {
        Span<byte> buffer = stackalloc byte[2];
        BinaryPrimitives.WriteInt16LittleEndian(buffer, value);
        _stream.Write(buffer);
    }
    public void Write(params ulong[] values)
    {
        foreach (var value in values)
            Write(value);
    }
    public void Write(ulong value)
    {
        Span<byte> buffer = stackalloc byte[8];
        BinaryPrimitives.WriteUInt64LittleEndian(buffer, value);
        _stream.Write(buffer);
    }
    public void Write(params ushort[] values)
    {
        foreach (var value in values) Write(value);
    }
    public void Write(uint value)
    {
        Span<byte> buffer = stackalloc byte[4];
        BinaryPrimitives.WriteUInt32LittleEndian(buffer, value);
        _stream.Write(buffer);
    }

    public void Write(params uint[] values)
    {
        foreach (var value in values)
            Write(value);
    }
    public void Write(int value)
    {
        Span<byte> buffer = stackalloc byte[4];
        BinaryPrimitives.WriteInt32LittleEndian(buffer, value);
        _stream.Write(buffer);
    }

    public void Write(params int[] values)
    {
        foreach (var value in values)
            Write(value);
    }


    public void Write(params short[] values)
    {
        foreach (var value in values)
            Write(value);
    }
    public void Write(float value)
    {
        Span<byte> buffer = stackalloc byte[4];
        BinaryPrimitives.WriteSingleLittleEndian(buffer, value);
        _stream.Write(buffer);
    }
    public void Write(params float[] values)
    {
        foreach (var value in values)
            Write(value);
    }
    public void Write(byte value) => _stream.WriteByte(value);
    public void Write(sbyte value) => _stream.WriteByte((byte)value);
    public void Write(ReadOnlySpan<byte> source) => _stream.Write(source);

    public void Write(string value, string encoderName = "ASCII")
    {
        var encoder = Encoding.GetEncoding(encoderName);
        var size = encoder.GetByteCount(value);
        Span<byte> buffer = stackalloc byte[size];
        encoder.GetBytes(value, buffer);
        _stream.Write(buffer);
        _stream.WriteByte(0x00);//Null Terminator
    }

    public void WriteFixedString(string value, int length, string encoderName = "Shift_JIS")
    {
        var encoder = Encoding.GetEncoding(encoderName);
        var size = encoder.GetByteCount(value);
        Span<byte> buffer = stackalloc byte[length];
        encoder.GetBytes(value, buffer);
        _stream.Write(buffer);
    }

    public void WriteFixedJisString(string value, int length) => WriteFixedString(value, length, "Shift_JIS");

    public void WriteFixedAsciiString(string value, int length) => WriteFixedString(value, length, "ASCII");
}

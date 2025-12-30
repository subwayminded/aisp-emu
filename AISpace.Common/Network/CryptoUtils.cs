using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Math;

namespace AISpace.Common.Network;

public class CryptoUtils
{
    private static readonly BigInteger RsaE = BigInteger.ValueOf(65537);

    // Rust: BigUint::from_bytes_le(bytes)
    private static BigInteger FromLeUnsigned(ReadOnlySpan<byte> le)
    {
        // BouncyCastle BigInteger expects big-endian magnitude
        byte[] be = le.ToArray();
        Array.Reverse(be);
        return new BigInteger(1, be);
    }

    // Rust: to_bytes_le() then copy into [0..len] and pad zeros
    private static byte[] ToFixedLe(BigInteger x, int size)
    {
        var be = x.ToByteArrayUnsigned(); // big-endian
        var le = new byte[size];

        int len = Math.Min(size, be.Length);
        for (int i = 0; i < len; i++)
            le[i] = be[be.Length - 1 - i]; // grab LSBs and output LE

        return le;
    }

        // Rust create_key::<16>: random 16 bytes, key[15]=0, reject if m>=n (m is LE integer)
    private static byte[] CreatePlainKeyLe16(BigInteger n)
    {
        Span<byte> key = stackalloc byte[16];

        while (true)
        {
            RandomNumberGenerator.Fill(key);
            key[15] = 0; // match Rust

            var m = FromLeUnsigned(key); // interpret bytes as LE integer
            if (m.CompareTo(n) >= 0)
                continue;

            return key.ToArray(); // plaintext key bytes (LE)
        }
    }

    public static (byte[] PlainKeyLe, byte[] EncryptedKeyLe) CreateEncryptedKey(byte[] rsaNLe)
    {
        var n = FromLeUnsigned(rsaNLe);          // FIX: modulus is LE on the wire
        var plainLe = CreatePlainKeyLe16(n);     // plaintext key bytes (LE)
        var m = FromLeUnsigned(plainLe);         // m = key as LE integer
        var c = m.ModPow(RsaE, n);               // c = m^e mod n
        var cipherLe = ToFixedLe(c, 16);         // fixed 16 bytes LE

        return (plainLe, cipherLe);
    }






    public static string GenerateOTP()
    {
        byte[] seed = Guid.NewGuid().ToByteArray();
        byte[] hash = SHA256.HashData(seed);
        var sb = new StringBuilder(hash.Length * 2);
        foreach (byte b in hash)
            sb.Append(b.ToString("x2"));
        string opt = sb.ToString(0, 20);
        return opt;
    }

}

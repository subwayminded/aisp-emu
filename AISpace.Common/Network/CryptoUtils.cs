using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Math;

namespace AISpace.Common.Network;

public class CryptoUtils
{
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
    public static byte[] CreateKey(int size, BigInteger maxVal)
    {
        byte[] keyData = new byte[size];

        // Fill with cryptographically strong random bytes
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(keyData);
        }

        // Ensure it's positive (BigInteger interprets byte[] as little-endian signed)
        byte[] unsignedKey = new byte[size + 1];
        Buffer.BlockCopy(keyData, 0, unsignedKey, 0, size);

        BigInteger candidate = new BigInteger(unsignedKey);

        // Reduce by modulus (introduces tiny bias if maxVal doesn't divide evenly)
        BigInteger reduced = candidate.Remainder(maxVal);
        if (reduced.SignValue < 0) reduced = reduced.Add(maxVal); // Just in case

        byte[] result = reduced.ToByteArrayUnsigned();
        if (result.Length < size)
            Array.Resize(ref result, size);
        else if (result.Length > size)
            Array.Resize(ref result, size);
        Array.Reverse(result);

        return result;
    }

    public static byte[] CreateCamelliaKey(byte[] buffer)
    {
        byte[] nLength = new byte[16];

        Array.Copy(buffer, nLength, 16);
        Array.Reverse(nLength);
        var n = new BigInteger(1, nLength.Reverse().ToArray());
        var key = CryptoUtils.CreateKey(16, n);



        var camelliaInt = new BigInteger(1, key.Reverse().ToArray());
        BigInteger E = new BigInteger("65537"); // RSA e
        var encS2C = camelliaInt.ModPow(E, n);

        byte[] camelliaKey = ToFixedLe(encS2C, 16);
        return camelliaKey;
    }

    // Helper: encode BigInteger -> fixed-size LE
    private static byte[] ToFixedLe(BigInteger bi, int size)
    {
        var be = bi.ToByteArrayUnsigned(); // big-endian, no sign
        var le = new byte[size];
        int copy = Math.Min(be.Length, size);
        for (int i = 0; i < copy; i++)
            le[i] = be[be.Length - 1 - i]; // reverse
        return le;
    }
}

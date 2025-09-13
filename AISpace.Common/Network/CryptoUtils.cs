using System.Security.Cryptography;
using Org.BouncyCastle.Math;

namespace AISpace.Common.Network;

internal class CryptoUtils
{
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
}

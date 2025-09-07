using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AISpace.Common.Network;

namespace AISpace.Common.Game
{
    public class CharaVisual(uint bloodType, byte month, byte day, uint gender, uint charaID, byte face, uint hairstyle)
    {
        public uint BloodType = bloodType;
        public byte Month = month;
        public byte Day = day;
        public uint Gender = gender;
        public uint CharaID = charaID;
        public byte Face = face;
        public uint Hairstyle = hairstyle;

        public byte[] ToBytes()
        {
            Span<byte> buffer = stackalloc byte[100];
            var writer = new PacketWriter(buffer);
            writer.WriteUInt32LE(BloodType);
            writer.WriteByte(Month);
            writer.WriteByte(Day);
            writer.WriteUInt32LE(Gender);
            writer.WriteUInt32LE(CharaID);
            writer.WriteByte(Face);
            writer.WriteUInt32LE(Hairstyle);
            return writer.WrittenBytes;
        }
    }
}

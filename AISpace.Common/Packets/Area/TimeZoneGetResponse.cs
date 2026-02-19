namespace AISpace.Common.Network.Packets;

public class TimeZoneGetResponse(uint Result, uint Timezone, float Time, uint TimeZoneMax, byte Flag) : IPacket<TimeZoneGetResponse>
{
    public static TimeZoneGetResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(Result);       // 0 = успех
        writer.Write(Timezone);     // 0=Утро, 1=День, 2=Вечер, 3=Ночь
        
        // Время часто передается как float (часы), либо int. 
        // Если float не сработает, попробуем (uint)Time.
        // В оригинале было uint, но для плавности солнца часто нужен float.
        // Пока оставим uint как в оригинале, но с реальным значением.
        writer.Write((uint)Time);   
        
        writer.Write(TimeZoneMax);  // Обычно 24 (длина цикла)
        writer.Write(Flag);         // Флаг принудительного обновления
        return writer.ToBytes();
    }
}
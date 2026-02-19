namespace AISpace.Common.Game;

public class AvatarData(uint Result, CharaData chara)
{
    public readonly uint Result = Result;
    public readonly CharaData chara = chara;

    public byte[] ToBytes()
    {
        var writer = new Network.PacketWriter();
        writer.Write(Result); // 4 байта
        writer.Write(chara.ToBytes()); // ~349 байт
        
        // ВАЖНО: Добиваем пакет до фиксированного размера, который ждет клиент
        // Пишем заголовок доп. данных (2 байта) и остаток (573 байта)
        writer.Write((ushort)8); 
        writer.Write(new byte[573]); 
        
        return writer.ToBytes(); // Итого ровно 928 байт
    }
}
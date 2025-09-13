namespace AISpace.Common.Network;

public record PacketContext(ClientContext Client, PacketType Type, byte[] Data, ushort RawType);

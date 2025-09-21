namespace AISpace.Common.Network;

public record Packet(ClientConnection Client, PacketType Type, byte[] Data, ushort RawType);

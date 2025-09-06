namespace AISpace.Common.Network;

public record PacketContext(ClientContext Client, ushort Type, byte[] Data);

namespace AISpace.Common.Network;

[Flags]
public enum MessageDomain
{
    Auth = 1 << 1,
    Area = 1 << 2,
    Msg = 1 << 3,
}

public interface IPacketHandler
{
    PacketType RequestType { get; }
    PacketType ResponseType { get; }
    MessageDomain Domains { get; }
    Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default);
}

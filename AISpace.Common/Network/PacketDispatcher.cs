namespace AISpace.Common.Network;

public class PacketDispatcher
{
    private readonly Dictionary<(MessageDomain, PacketType), IPacketHandler> _handlers;

    public PacketDispatcher(IEnumerable<IPacketHandler> allHandlers, MessageDomain activeDomains)
    {
        _handlers = [];

        foreach (var handler in allHandlers)
        {
            foreach (MessageDomain domain in Enum.GetValues<MessageDomain>())
            {
                if (activeDomains.HasFlag(domain) && handler.Domains.HasFlag(domain))
                {
                    _handlers[(domain, handler.RequestType)] = handler;
                }
            }
        }
    }

    public async Task DispatchAsync(MessageDomain domain, PacketType type, byte[] payload, ClientConnection connection, CancellationToken ct = default)
    {
        if (_handlers.TryGetValue((domain, type), out var handler))
        {
            await handler.HandleAsync(payload, connection, ct);
        }
        else
        {
            Console.WriteLine($"No handler for {domain}:{type}");
        }
    }
}

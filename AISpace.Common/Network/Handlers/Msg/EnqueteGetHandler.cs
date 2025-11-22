
using AISpace.Common.Game;
using AISpace.Common.Network.Packets.Msg;

namespace AISpace.Common.Network.Handlers.Msg;

public class EnqueteGetHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.Msg_EnqueteGetRequest;

    public PacketType ResponseType => PacketType.Msg_EnqueteGetResponse;

    public MessageDomain Domains => MessageDomain.Msg;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        Console.WriteLine("Creating Quiz");
        //Payload is empty.
        List<EnqueteData> quizData = [];
        EnqueteData test = new EnqueteData(0, "What is the music of life?");
        test.answers.Add("Um... the lute? No, drums!");
        test.answers.Add("Screaming?");
        test.answers.Add("Silence, my brother");
        test.answers.Add("Some kind of choir. With chanting");
        quizData.Add(test);
        EnqueteGetResponse enqueteGetResponse = new(0, quizData);
        Console.WriteLine("Sending Quiz");
        await connection.SendAsync(ResponseType, enqueteGetResponse, ct);
    }
}

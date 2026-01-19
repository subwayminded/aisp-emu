
using AISpace.Common.DAL.Entities;
using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Msg;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Msg;

public class AvatarCreateHandler(ILogger<AvatarCreateHandler> logger, ICharacterRepository charRepo) : IPacketHandler
{
    public PacketType RequestType => PacketType.AvatarCreateRequest;

    public PacketType ResponseType => PacketType.AvatarCreateResponse;

    public MessageDomain Domains => MessageDomain.Msg;

    private readonly ILogger<AvatarCreateHandler> _logger = logger;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var createRequest = AvatarCreateRequest.FromBytes(payload.Span);

        _logger.LogInformation("createRequest: {request}", createRequest.ToString());

        //TODO: Send Logout request?
        if (!connection.IsAuthenticated || connection.User == null)
            return;

        Character newChar = await charRepo.CreateAsync(createRequest.AvatarName,
            connection.User.Id,
            createRequest.modelId,
            createRequest.visual.BloodType,
            createRequest.visual.Birthdate,
            (int)createRequest.visual.Gender,
            createRequest.visual.Face,
            createRequest.visual.Hairstyle, ct);

        //Add Clothing
        if ((int)createRequest.visual.Gender == 1)
        {
            //Male
            await charRepo.EquipAsync(newChar.Id, 0, 10100220, ct);//Shirt
            await charRepo.EquipAsync(newChar.Id, 1, 10200100, ct);//Pants
            await charRepo.EquipAsync(newChar.Id, 2, 10400030, ct);//Socks
            await charRepo.EquipAsync(newChar.Id, 3, 10500070, ct);//Shoes
        }
        else
        {
            //Female
            await charRepo.EquipAsync(newChar.Id, 0, 10100060, ct);//Shirt
            await charRepo.EquipAsync(newChar.Id, 1, 10200090, ct);//Pants
            await charRepo.EquipAsync(newChar.Id, 2, 10400000, ct);//Socks
            await charRepo.EquipAsync(newChar.Id, 3, 10500010, ct);//Shoes
        }

            //TODO: do something with the avatar creation request
            await connection.SendAsync(ResponseType, new AvatarCreateResponse(0).ToBytes(), ct);
    }
}

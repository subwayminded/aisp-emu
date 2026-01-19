
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
        if (!connection.IsAuthenticated)
            return;

        Character newChar = await charRepo.CreateAsync(createRequest.AvatarName,
            connection.clientUser!.Id,
            createRequest.modelId,
            createRequest.visual.BloodType,
            createRequest.visual.Birthdate,
            (int)createRequest.visual.Gender,
            createRequest.visual.Face,
            createRequest.visual.Hairstyle, ct);

        //Add Clothing
        //DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10100220, 0));//Shirt
        //DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10200100, 0));//Pants
        //DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10400030, 0));//Socks
        //DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10500070, 0));//Shoes

        //DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10100060, 0));//Shirt
        //DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10200090, 0));//Shorts
        //DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10400000, 0));//Socks
        //DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10500010, 0));//Shoes
        if ((int)createRequest.visual.Gender == 1)
        {
            //Male
            await charRepo.EquipAsync(newChar.Id, 0, 10100220, ct);
            await charRepo.EquipAsync(newChar.Id, 1, 10200100, ct);
            await charRepo.EquipAsync(newChar.Id, 2, 10400030, ct);
            await charRepo.EquipAsync(newChar.Id, 3, 10500070, ct);
        }
        else
        {
            //Female
            await charRepo.EquipAsync(newChar.Id, 0, 10100060, ct);
            await charRepo.EquipAsync(newChar.Id, 1, 10200090, ct);
            await charRepo.EquipAsync(newChar.Id, 2, 10400000, ct);
            await charRepo.EquipAsync(newChar.Id, 3, 10500010, ct);
        }

            //TODO: do something with the avatar creation request
            await connection.SendAsync(ResponseType, new AvatarCreateResponse(0).ToBytes(), ct);
    }
}

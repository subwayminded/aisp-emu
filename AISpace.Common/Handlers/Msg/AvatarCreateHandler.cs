using AISpace.Common.DAL.Entities;
using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Msg;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Msg;

public class AvatarCreateHandler(ILogger<AvatarCreateHandler> logger, ICharacterRepository charRepo) : PacketHandlerBase<AvatarCreateRequest, AvatarCreateResponse>
{
    public override PacketType RequestType => PacketType.AvatarCreateRequest;
    public override PacketType ResponseType => PacketType.AvatarCreateResponse;
    public override MessageDomain Domain => MessageDomain.Msg;

    public override async Task<AvatarCreateResponse?> HandleAsync(AvatarCreateRequest request, ClientConnection connection, CancellationToken ct = default)
    {
        if (connection.User == null) return null;

        logger.LogInformation($"[CREATE] Character '{request.AvatarName}' Gender: {request.visual.Gender}");

        // 1. Создаем персонажа
        var newChar = await charRepo.CreateAsync(
            request.AvatarName, connection.User.Id, request.modelId, 
            request.visual.BloodType, request.visual.Birthdate, 
            (int)request.visual.Gender, request.visual.Face, request.visual.Hairstyle, ct);

        // 2. АВТО-ЭКИПИРОВКА СТАРТОВОГО НАБОРА (чтобы не были голыми)
        if (request.visual.Gender == 1) // Мужчина
        {
            await charRepo.EquipAsync(newChar.Id, 0, 10100220, ct); // Рубашка
            await charRepo.EquipAsync(newChar.Id, 1, 10200100, ct); // Брюки
            await charRepo.EquipAsync(newChar.Id, 3, 10500070, ct); // Обувь
        }
        else // Женщина
        {
            await charRepo.EquipAsync(newChar.Id, 0, 10100060, ct); // Жен. рубашка
            await charRepo.EquipAsync(newChar.Id, 1, 10200090, ct); // Жен. шорты
            await charRepo.EquipAsync(newChar.Id, 3, 10500010, ct); // Жен. туфли
        }

        return new AvatarCreateResponse(0);
    }
}
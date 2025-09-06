namespace AISpace.Common.Network;

public enum PacketType: ushort
{
    //Version
    VersionCheckRequest = 0x62BC,
    VersionCheckResponse = 0xB6B4,
    //Auth
    AuthenticateRequest = 0xF24B,
    AuthenticateResponse = 0xD4AB,
    //Ping
    PingRequest = 0xC202,
    PingResponse = 0xC202,
    //World
    WorldListRequest = 0x6676,
    WorldListResponse = 0xEE7E,
    WorldSelectRequest = 0x7FE7,
    WorldSelectResponse = 0x3491,
    //Item
    ItemGetBaseListRequest = 0xC8EA,
    ItemGetBaseListResponse = 0xC7A9,
    ItemGetListRequest = 0x2A9A,
    ItemGetListResponse = 0xA522,
    //Login
    LoginRequest = 0x34EF,
    LoginResponse = 0x1FEA,
    LogoutRequest = 0x0AD0,
    //Avatar
    AvatarCreateRequest = 0x29A4,
    AvatarCreateResponse = 0x788F,
    AvatarDestroyRequest = 0x765A,
    AvatarGetCreateInfoRequest = 0x04F6,
    AvatarGetDataRequest = 0xAD9E,
    AvatarGetDataResponse = 0xB055,
    AvatarDataResponse = 0x6747,
    AvatarNotifyData = 0x7D78,
    AvatarSelectRequest = 0x113D,
    AvatarSelectResponse = 0x2C5F,
    AvatarGetCreateInfoResponse = 0xA5AD,
    AvatarMove = 0x9483
}

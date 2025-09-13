namespace AISpace.Common.Network;
public enum PacketType: ushort
{
    //Adventure
    AdventureUploadRateGetRequest = 0x71CF,
    AdventureUploadRateGetResponse = 0x9061,
    //AI
    AiDownloadListGetRequest = 0x1D3F,
    AiDownloadListGetResponse = 0xBEE1,
    AiUploadRateGetRequest = 0xE30D,
    AiUploadRateGetResponse = 0xB2BC,
    //Area
    AreasvEnterRequest = 0x4646,
    AreasvEnterResponse = 0x0149,
    AreasvLeaveRequest = 0xF7B9,
    AreasvLeaveResponse = 0xE31D,
    //Auth
    AuthenticateRequest = 0xF24B, //authenticate
    AuthenticateResponse = 0xD4AB,
    //Avatar
    AvatarCreateRequest = 0x29A4,
    AvatarCreateResponse = 0x788F,
    AvatarDataResponse = 0x6747,
    AvatarDestroyRequest = 0x765A,
    AvatarGetCreateInfoRequest = 0x04F6,
    AvatarGetCreateInfoResponse = 0xA5AD,
    AvatarGetDataRequest = 0xAD9E,
    AvatarGetDataResponse = 0xB055,
    NotifyMoveData = 0x9483,//6d28
    AvatarNotifyData = 0x7D78,
    AvatarNotifyData2 = 0x6D28,
    AvatarSelectRequest = 0x113D,
    AvatarSelectResponse = 0x2C5F,
    //Channel
    ChannelListGetRequest = 0x0300,
    ChannelListGetResponse = 0xF27F,
    ChannelSelectRequest = 0xFFE1,
    ChannelSelectResponse = 0xFFEA,
    //Circle
    CircleChangeCoreAuthorityRequest = 0x05ED,
    CircleChangeCoreAuthorityResponse = 0xC097,
    CircleChatInRequest = 0x9514,
    CircleChatInResponse = 0x81C6,
    CircleChatOutRequest = 0x05E5,
    CircleChatPostRequest = 0x3D7F,
    CircleChatPostResponse = 0xA9C1,
    CircleCreateRequest = 0x1048,
    CircleGetDataRequest = 0xDB5F,
    CircleGetDataResponse = 0x90AD,
    CircleLeaderChangeResponse = 0xBB59,
    CircleMarkChangeRequest = 0xD895,
    CircleMarkChangeResponse = 0xD0EF,
    CircleMemberJoinAnswerRequest = 0x1B70,
    CircleMemberJoinMemberCancelRequest = 0x83C1,
    CircleMemberJoinMemberRequest = 0xAB2D,
    CircleMemberJoinMemberResponse = 0xDC3A,
    CircleMemberKickRequest = 0xBF32,
    CircleMessageChangeRequest = 0x2D2B,
    CircleNotiftyChatOut = 0xBBC4,
    CircleNotifyChatIn = 0xCBFA,
    CircleNotifyJoinRequest = 0x9888,
    CircleNotifyJoinRequestResult = 0x8FED,
    CircleNotifyMember = 0xBF0E,
    CircleResignRequest = 0x7382,
    //Emotion
    EmotionGetBaseListRequest = 0x7FCD,
    EmotionGetBaseListResponse = 0x28E3,
    EmotionGetObtainedListRequest = 0xFD42,
    EmotionGetObtainedListResponse = 0xC3D7,
    //Equip
    EquipOrderListRequest = 0xF74C,
    EquipOrderListResponse = 0x2DAE,
    //Friend
    FriendGetListDataRequest = 0x805F,
    FriendGetListDataResponse = 0x2411,
    FriendLinkTagGetFreeRequest = 0xC88F,
    FriendLinkTagGetRequest = 0x0F97,
    FriendLinkTagGetResponse = 0x239E,
    //Furniture
    FurnitureGetBaseListRequest = 0x2FDA,
    FurnitureGetBaseListResponse = 0xA0D1,
    //Heroine
    HeroineGetTicketBaseRequest = 0x25CE,
    HeroineGetTicketBaseResponse = 0x16E6,
    //Item
    ItemGetBaseListRequest = 0xC8EA,
    ItemGetBaseListResponse = 0xC7A9,
    ItemGetListRequest = 0x2A9A,
    ItemGetListResponse = 0xA522,
    //Login
    LoginRequest = 0x34EF,
    LoginResponse = 0x1FEA,
    LogoutRequest = 0x0AD0,
    //MailBox
    MailBoxGetDataRequest = 0x8D92,
    MailBoxGetDataResponse = 0x147A,
    MailDeleteRequest = 0xF96D,
    MailDeleteResponse = 0xE501,
    MailOpenRequest = 0x1292,
    MailOpenResponse = 0xDF76,
    MailPostRequest = 0x34BC,
    MailProtectCancelRequest = 0xFEAD,
    MailProtectRequest = 0x024C,
    MailProtectResponse = 0xC3E4,
    //Map
    MapDataEnterEndRequest = 0x04B4,
    MapDataEnterEndResponse = 0xBE02,
    MapEnterRequest = 0x2810,
    MapEnterResponse = 0x1DCD,
    MapLinkGetDataRequest = 0x30C8,
    MapLinkGetDataResponse = 0x6C4E,
    MapLinkNotifyData = 0x5755,
    //Mascot
    MascotGetCountRequest = 0x0CBC,
    MascotGetCountResponse = 0x7790,
    //Mission
    MissionDataRequest = 0x7D29,
    MissionDataResponse = 0x47F9,
    //Money
    MoneyDataGetRequest = 0x61E7,
    MoneyDataGetResponse = 0xDC19,
    MoneyNpsPointsRequest = 0xBF17,
    MoneyNpsPointsResponse = 0x3CF5,
    //MyRoom
    MyRoomGetFurnitureRequest = 0xE868,
    MyRoomGetFurnitureResponse = 0x943D,
    MyRoomNotifyFurniture = 0xA64A,
    //Niconi
    NiconiCommonsBaseListRequest = 0x97B7,
    NiconiCommonsBaseListResponse = 0xE60C,
    //NPC
    NpcGetDataRequest = 0x461B,
    NpcGetDataResponse = 0x4403,
    NpcNotifyData = 0xCD67,
    //Ping
    Ping = 0xC202,

    PostTalkRequest = 0xEB2E,
    //Robo
    RoboGetListRequest = 0x44CE,
    RoboGetListResponse = 0xF606,
    RoboGetObtainedSkillListRequest = 0xDCBF,
    RoboGetObtainedSkillListResponse = 0x1159,
    RoboVoiceTypeUpdateRequest = 0x9305,
    RoboVoiceTypeUpdateResponse = 0x8F10,
    //Timezone
    TimeZoneGetRequest = 0x5F53,
    TimeZoneGetResponse = 0xCD38,
    //Ucc
    UccAdvFigureBaseListRequest = 0x86DD,
    UccAdvFigureBaseListResponse = 0x878A,
    UccVoiceBaseListRequest = 0x1149,
    UccVoiceBaseListResponse = 0xBB8F,
    //Update Option
    UpdateOptionRequest = 0x79A1,
    UpdateOptionResponse = 0xB314,
    //Version
    VersionCheckRequest = 0x62BC, //CProtoAuth_client::recv_check_version_r
    VersionCheckResponse = 0xB6B4, //CProtoAuth_client::send_check_version
    //World
    WorldListRequest = 0x6676, //CProtoAuth_client::send_get_worldlist
    WorldListResponse = 0xEE7E, //CProtoAuth_client::recv_get_worldlist_r
    WorldSelectRequest = 0x7FE7, //CProtoAuth_client::send_select_world
    WorldSelectResponse = 0x3491, //CProtoAuth_client::recv_select_world_r
}
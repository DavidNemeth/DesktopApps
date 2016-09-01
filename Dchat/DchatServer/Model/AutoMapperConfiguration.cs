using AutoMapper;
using DchatEntities;

namespace DchatServer.Model
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, DmUser>();
                cfg.CreateMap<ChatRoom, DmChatRoom>();
            });
            IMapper mapper = config.CreateMapper();
            var usersource = new User();
            var roomsource = new ChatRoom();
            var dest = mapper.Map<User, DmUser>(usersource);
            var dest2 = mapper.Map<ChatRoom, DmChatRoom>(roomsource);
        }
    }
}

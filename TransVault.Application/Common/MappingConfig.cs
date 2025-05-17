using AutoMapper;
namespace TransfloUser;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<UserDTO, User>();

        CreateMap<TicketDTO, Ticket>();

        CreateMap<TicketNoteDTO, TicketNote>();

        CreateMap<AttachmentDTO, Attachment>();


        CreateMap<User, UserDTO>().ReverseMap();

        CreateMap<Ticket, TicketDTO>().ReverseMap();

        CreateMap<TicketNote, TicketNoteDTO>().ReverseMap();

        CreateMap<Attachment, AttachmentDTO>().ReverseMap();
    }
}

using Bingogo.Data.Entities;
using Bingogo.Models;

namespace Bingogo.Services.Profiles;

public class BoardProfile : Profile
{
    public BoardProfile()
    {
        CreateMap<Board, BoardModel>().ReverseMap();
        CreateMap<Board, BoardForm>().ReverseMap();
        CreateMap<Board, BoardProps>().ReverseMap();
    }
}

using Bingogo.Data.Entities;
using Bingogo.Models;

namespace Bingogo.Services.Profiles;

public class BoardTileProfile : Profile
{
    public BoardTileProfile()
    {
        CreateMap<BoardTile, BoardTileModel>().ReverseMap();
        CreateMap<BoardTile, BoardTileForm>().ReverseMap();
        CreateMap<BoardTile, BoardTileProps>().ReverseMap();
    }
}

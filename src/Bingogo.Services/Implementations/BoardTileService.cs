using Bingogo.Data.Entities;
using Bingogo.Services.Base;
using Bingogo.Services.Interfaces;

namespace Bingogo.Services.Implementations;

public class BoardTileService : ResourceService<BoardTile, long>, IBoardTileService
{
    public BoardTileService(DbContext context, string key = null) : base(context, key) { }
}

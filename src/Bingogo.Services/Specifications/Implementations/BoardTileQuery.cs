using Bingogo.Data.Entities;
using Bingogo.Services.Specifications.Base;
using Bingogo.Services.Specifications.Interfaces;

namespace Bingogo.Services.Specifications.Implementations;

public class BoardTileQuery : Specification<BoardTile>, ISpecification<BoardTile>
{
    public long[]? Ids { get; set; }
    public long? BoardId { get; set; }

    protected override Expression<Func<BoardTile, bool>> GetPredicate()
    {
        return boardTile =>
            (Ids == null || Ids.Contains(boardTile.Id)) &&
            (BoardId == null || BoardId == boardTile.BoardId);
    }
}

using Bingogo.Data.Entities;
using Bingogo.Models.Enums;
using Bingogo.Services.Specifications.Base;
using Bingogo.Services.Specifications.Interfaces;

namespace Bingogo.Services.Specifications.Implementations;

public class BoardQuery : Specification<Board>, ISpecification<Board>
{
    public long[]? Ids { get; set; }
    public string[]? Categories { get; set; }
    public GameMode? GameMode { get; set; }
    public GameType? GameType { get; set; }

    protected override Expression<Func<Board, bool>> GetPredicate()
    {
        return board =>
            (Ids == null || Ids.Contains(board.Id)) &&
            (Categories == null || Categories.Contains(board.Category)) &&
            (GameMode == null || GameMode.Value.HasFlag(board.GameMode)) &&
            (GameType == null || GameType.Value.HasFlag(board.GameType));
    }
}

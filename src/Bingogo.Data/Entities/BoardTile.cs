using Bingogo.Core.Data;

namespace Bingogo.Data.Entities;

public class BoardTile : IEntity<long>
{
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the visible title for the tile.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or set if the tile is checked.
    /// </summary>
    public bool IsChecked { get; set; }

    /// <summary>
    /// Gets or sets the points for a checked tile.
    /// </summary>
    public int Points { get; set; }

    /// <summary>
    /// Gets or sets the board foreign key identifier 
    /// </summary>
    public long BoardId { get; set; }
    public Board Board { get; set; }
}

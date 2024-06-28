using Bingogo.Core.Data;
using Bingogo.Models.Enums;

namespace Bingogo.Data.Entities;

public class Board : IEntity<long>, IHistorical
{
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the title for the board.
    /// </summary>
    /// <example>
    /// Papich bingo, Tekken bingo 
    /// </example>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets if the board is available publicly of only via share link.
    /// </summary>
    /// <remarks>
    /// Sharable boards are not implmeneted currenly. 
    /// </remarks>
    public bool IsPublic { get; set; }

    /// <summary>
    /// In short, events happen after some amount of tiles are checked.
    /// Events are just an interactive way to make the gaming process harder. 
    /// This idea came to me on 28/06/2024
    /// </summary>
    /// <remarks>
    /// Currenlty no implementation for events exist. 
    /// </remarks>
    public bool HasEvents { get; set; }

    /// <summary>
    /// Gets or sets the game mode.
    /// </summary>
    public GameMode GameMode { get; set; }

    /// <summary>
    /// Gets or sets the game type.
    /// </summary>
    public GameType GameType { get; set; }

    /// <summary>
    /// Gets or sets the board tiles.
    /// </summary>
    public ICollection<BoardTile> Tiles { get; set; }

    public long? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public long? UpdatedById { get; set; }
    public long? DeletedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}

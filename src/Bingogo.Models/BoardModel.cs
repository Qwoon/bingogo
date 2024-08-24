using Bingogo.Models.Enums;

namespace Bingogo.Models;

public class BoardModel : BoardForm, IResource<long>
{
    public long Id { get; set; }

    public new IEnumerable<BoardTileModel> Tiles { get; set; }

    public long? CreatedById { get; set; }
    public UserModel? CreatedBy { get; set; }
    public long? UpdatedById { get; set; }
    public long? DeletedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}

public class BoardForm : BoardProps
{
    public string Category { get; set; }
    public virtual IEnumerable<BoardTileForm> Tiles { get; set; }
}

public class BoardProps
{
    public string Title { get; set; }
    public bool IsPublic { get; set; }
    public bool HasEvents { get; set; }
    public GameMode GameMode { get; set; }
    public GameType GameType { get; set; }
}

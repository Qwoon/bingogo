namespace Bingogo.Models;

public class BoardTileModel : BoardTileForm, IResource<long>
{
    public long Id { get; set; }
}

public class BoardTileForm : BoardTileProps
{
    public long BoardId { get; set; }
    public int Points { get; set; }
}
public class BoardTileProps
{
    public string Title { get; set; }
    public bool IsChecked { get; set; }
}

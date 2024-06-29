using Bingogo.Data.Entities;
using Bingogo.Services.Base;
using Bingogo.Services.Interfaces;

namespace Bingogo.Services.Implementations;

public class BoardService : ResourceService<Board, long>, IBoardService
{
    public BoardService(DbContext context, string key = null) : base(context, key) { }
}


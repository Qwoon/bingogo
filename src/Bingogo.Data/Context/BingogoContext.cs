using Bingogo.Data.Configuration;
using Bingogo.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bingogo.Data.Context;

public class BingogoContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    public BingogoContext() { }

    public BingogoContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDatabase();
    }

    public virtual DbSet<Board> Boards { get; set; }
    public virtual DbSet<BoardTile> BoardsTiles { get; set; }
}

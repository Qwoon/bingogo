using Bingogo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bingogo.Data.Configuration;

/// <summary>
/// Represents extension method for configuring the database tables & relations.
/// Please stick to the following template when configuring new resoures:
/// 1. Primary Key
/// 2. Properties
/// 3. Relations: 
///     3.1. HasOne > HasMany, so make sure to write relations where the foreign key is located.
/// </summary>
public static class DBConfigurator
{
    public static void ConfigureDatabase(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .ConfigureUsers()
            .ConfigureBoards();
    }

    private static ModelBuilder ConfigureUsers(this ModelBuilder modelBuilder)
    {
        #region Users

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(x => x.Level)
                .HasDefaultValue(1);
        });

        #endregion

        return modelBuilder;
    }

    private static ModelBuilder ConfigureBoards(this ModelBuilder modelBuilder)
    {
        #region Boards

        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                .IsRequired();

            entity.Property(x => x.IsPublic)
                .HasDefaultValue(true);

            entity.Property(x => x.HasEvents)
                .HasDefaultValue(false);

            entity.Property(x => x.GameMode)
                .HasDefaultValue(1);

            entity.Property(x => x.GameType)
                .HasDefaultValue(1);

            entity.HasOne(x => x.CreatedBy)
                .WithMany(x => x.Boards)
                .HasForeignKey(x => x.CreatedById);
        });

        #endregion

        #region Board Tiles

        modelBuilder.Entity<BoardTile>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                .IsRequired();

            entity.Property(x => x.IsChecked)
                .HasDefaultValue(false);

            entity.Property(x => x.Points)
                .HasDefaultValue(0);

            entity.HasOne(x => x.Board)
                .WithMany(x => x.Tiles)
                .HasForeignKey(x => x.BoardId);
        });

        #endregion
        return modelBuilder;
    }
}

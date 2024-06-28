using Bingogo.Core.Data;
using Microsoft.AspNetCore.Identity;

namespace Bingogo.Data.Entities;

/// <summary>
/// Represents the user entity
/// </summary>
public class User : IdentityUser<long>, IEntity<long>
{
    /// <summary>
    /// Gets or sets the user level.
    /// </summary>
    /// <remarks>
    /// Levels are currenly not implemented and not used. 
    /// </remarks>
    public int Level { get; set; }
    /// <summary>
    /// Gets or sets the boards that the user has created.
    /// </summary>
    public ICollection<Board> Boards { get; set; }
}

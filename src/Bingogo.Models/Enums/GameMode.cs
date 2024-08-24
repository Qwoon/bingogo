namespace Bingogo.Models.Enums;

/// <summary>
/// Represents different game modes.
/// Game modes can be changed.
/// </summary>
public enum GameMode : byte
{
    /// <summary>
    /// Single-player mode. 
    /// </summary>
    SinglePlayer = 1,

    /// <summary>
    /// Multi-player mode. Other users can join. 
    /// </summary>
    MultiPlayer = 2
}

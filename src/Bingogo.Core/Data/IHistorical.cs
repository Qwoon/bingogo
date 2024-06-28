namespace Bingogo.Core.Data;

public interface IHistorical
{
    /// <summary>
    /// Gets or sets who created the entity.
    /// </summary>
    long? CreatedById { get; set; }

    /// <summary>
    /// Gets or sets who updated the entity.
    /// </summary>
    long? UpdatedById { get; set; }

    /// <summary>
    /// Gets or sets who deleted the entity.
    /// </summary>
    long? DeletedById { get; set; }

    /// <summary>
    /// Gets or sets the datetime stamp at which the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the datetime stamp at which the entity was updated.
    /// </summary>
    DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the datetime stamp at which the entity was deleted.
    /// </summary>
    DateTime DeletedAt { get; set; }
}

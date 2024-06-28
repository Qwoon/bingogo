namespace Bingogo.Core.Data;

public interface IEntity { }

public interface IEntity<T> : IEntity
{
    /// <summary>
    /// Represents the entity primary key.
    /// </summary>
    public T Id { get; set; }
}
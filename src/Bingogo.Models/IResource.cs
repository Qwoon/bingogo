using Bingogo.Core.Data;

namespace Bingogo.Models;

public interface IResource<T> : IResource, IEntity<T> { }

public interface IResource : IEntity { }

namespace Bingogo.Core.Extensions;

public static class EnumerableExtensions
{
    public static ICollection<T> AsCollection<T>(this IEnumerable<T> source)
    {
        return source as ICollection<T> ?? new List<T>(source);
    }
}
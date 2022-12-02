using SourceMapper.Models;

namespace SourceMapper;

public static class MappingExtensions
{
    public static QueryableMap<TFrom> Map<TFrom>(this IQueryable<TFrom> list) where TFrom : class
    {
        return new(list);
    }

    public static EnumerableMap<TFrom> Map<TFrom>(this List<TFrom> list) where TFrom : class
    {
        return new(list);
    }

    public static EnumerableMap<TFrom> Map<TFrom>(this IEnumerable<TFrom> list) where TFrom : class
    {
        return new(list);
    }

    public static SingleMap<TFrom> Map<TFrom>(this TFrom item)
    {
        return new(item);
    }
}

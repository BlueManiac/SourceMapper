using SourceMapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SourceMapper;

public static class MappingExtensions
{
    public static QueryableMap<TFrom> Map<TFrom>(this IQueryable<TFrom> list) where TFrom : class
    {
        return new QueryableMap<TFrom>(list);
    }

    public static EnumerableMap<TFrom> Map<TFrom>(this List<TFrom> list) where TFrom : class
    {
        return new EnumerableMap<TFrom>(list);
    }

    public static EnumerableMap<TFrom> Map<TFrom>(this IEnumerable<TFrom> list) where TFrom : class
    {
        return new EnumerableMap<TFrom>(list);
    }

    public static SingleMap<TFrom> Map<TFrom>(this TFrom item)
    {
        return new SingleMap<TFrom>(item);
    }
}

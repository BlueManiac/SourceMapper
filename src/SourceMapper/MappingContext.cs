using System.Linq.Expressions;

namespace SourceMapper;

public sealed class MappingContext<TFrom, TTo>
{
    public void Map<TValue>(Expression<Func<TTo, TValue>> select, Func<TFrom, TValue> map)
    {
        throw new NotImplementedException();
    }
}
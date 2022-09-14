namespace SourceMapper.Models;

public readonly struct QueryableMap<TFrom>
{
    private readonly IQueryable<TFrom> _list;

    internal QueryableMap(IQueryable<TFrom> list)
    {
        _list = list;
    }

    public IQueryable<TTo> To<TTo>() where TTo : IMapping<TFrom, TTo>
    {
        return _list.Select(TTo.MapFromExpression);
    }
}

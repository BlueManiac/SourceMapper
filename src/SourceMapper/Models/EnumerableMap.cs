namespace SourceMapper.Models;

public readonly struct EnumerableMap<TFrom>
{
    private readonly IEnumerable<TFrom> _list;

    internal EnumerableMap(IEnumerable<TFrom> list)
    {
        _list = list;
    }

    public IEnumerable<TTo> To<TTo>() where TTo : IMapping<TFrom, TTo>
    {
        return _list.Select(TTo.MapFrom);
    }
}

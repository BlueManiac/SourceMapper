using System.Linq.Expressions;

namespace SourceMapper;

public interface IMapping<TFrom, TTo>
{
    static abstract Expression<Func<TFrom, TTo>> MapFromExpression { get; }
    static abstract Func<TFrom, TTo> MapFrom { get; }

    protected static virtual void Configure(MappingContext<TFrom, TTo> context)
    {
    }
}

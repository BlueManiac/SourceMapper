using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SourceMapper;

public interface IMapping<TFrom, TTo>
{
    static abstract Expression<Func<TFrom, TTo>> MapFromExpression { get; }
    static abstract Func<TFrom, TTo> MapFrom { get; }
}

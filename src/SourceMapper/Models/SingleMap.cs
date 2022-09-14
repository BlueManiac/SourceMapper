using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceMapper.Models
{
    public readonly struct SingleMap<TFrom>
    {
        private readonly TFrom _item;

        internal SingleMap(TFrom item)
        {
            _item = item;
        }

        public TTo To<TTo>() where TTo : IMapping<TFrom, TTo>
        {
            return TTo.MapFrom(_item);
        }
    }
}

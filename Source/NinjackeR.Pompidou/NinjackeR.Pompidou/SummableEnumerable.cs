using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NinjackeR.Pompidou
{
    public struct SummableEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _underlying;

        public static IEnumerable<T> operator +(SummableEnumerable<T> e1, IEnumerable<T> e2)
        {
            return e1.Concat(e2);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _underlying.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _underlying.GetEnumerator();
        }

        public SummableEnumerable(IEnumerable<T> underlying)
        {
            _underlying = underlying;
        }
    }
}
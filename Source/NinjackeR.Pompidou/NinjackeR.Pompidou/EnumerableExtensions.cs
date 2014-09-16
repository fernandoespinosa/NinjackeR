using System.Collections.Generic;

namespace NinjackeR.Pompidou
{
    public static class EnumerableExtensions
    {
        public static SummableEnumerable<T> AsSummable<T>(this IEnumerable<T> enumerable)
        {
            return new SummableEnumerable<T>(enumerable);
        }

        public static string StringJoin<T>(this IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }
    }
}
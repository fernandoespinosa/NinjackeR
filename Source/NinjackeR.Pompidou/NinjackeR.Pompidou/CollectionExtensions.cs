using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NinjackeR.Pompidou
{
    public static class CollectionExtensions
    {
        public static void AddAll<T>(this ICollection<T> collection, IEnumerable<T> elements)
        {
            foreach (var e in elements)
                collection.Add(e);
        }

        public static void AddAll<T>(this ICollection<T> collection, params T[] elements)
        {
            foreach (var e in elements)
                collection.Add(e);
        }

        public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach (var e in elements)
                action(e);
        }

        public static void ForEach<T>(this IEnumerable<T> elements, Action<T, int> action)
        {
            int i = 0;
            elements.ForEach(e => action(e, i++));
        }

        public static string FormatSequence<T>(this IEnumerable<T> elements, string format)
        {
            return string.Format(format, elements.Cast<object>().ToArray());
        }

        public static string ToString<T>(this IEnumerable<T> elements, string separator)
        {
            return string.Join(separator, elements);
        }

        public static IDictionary<TKey, object> OfKeyType<TKey>(this IDictionary dictionary)
        {
            return dictionary.Cast<DictionaryEntry>().Where(e => e.Key is TKey).ToDictionary(e => (TKey) e.Key, e => e.Value);
        }

        public static IDictionary<TKey, TValue> OfKeyValueType<TKey, TValue>(this IDictionary dictionary)
        {
            return dictionary.Cast<DictionaryEntry>().Where(e => e.Key is TKey && e.Value is TValue).ToDictionary(e => (TKey) e.Key, e => (TValue) e.Value);
        }

        public static IDictionary<TKey, TValue> WhereKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, bool> predicate)
        {
            return dictionary.Where(e => predicate(e.Key)).ToDictionary(e => e.Key, e => e.Value);
        }

        public static IDictionary<TKey, TValue> WhereKeyValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            return dictionary.Where(predicate).ToDictionary(e => e.Key, e => e.Value);
        }

        public static IEnumerable<T> InduceSequence<T>(this T first, Func<T, T> inductor) where T : class
        {
            var list = new List<T>();
            for (var t = first; t != null; t = inductor(t))
            {
                list.Add(t);
            }
            return list;
        }

        public static void Enumerate<T>(this IEnumerable<T> enumerable)
        {
            enumerable.ToList();
        }
    }
}

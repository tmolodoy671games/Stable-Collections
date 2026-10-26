using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public static class StableCollectionKotlinExtensions
{
	public static IPersistentStableList<T> Take<T>(this IStableCollection<T> source, int n)
	{
		StableEnumerableExtensions.RequireNonNegative(n, "n");
		return Enumerable.Take(source, n).ToPersistentStableList();
	}

	public static IPersistentStableList<T> TakeWhile<T>(this IStableCollection<T> source, Func<T, bool> predicate)
	{
		return Enumerable.TakeWhile(source, predicate).ToPersistentStableList();
	}

	public static IPersistentStableList<T> Distinct<T>(this IStableCollection<T> source)
	{
		HashSet<T> hashSet = new HashSet<T>();
		return source.Filter(hashSet.Add);
	}

	public static IPersistentStableList<T> DistinctBy<T, TKey>(this IStableCollection<T> source, Func<T, TKey> selector)
	{
		HashSet<TKey> seen = new HashSet<TKey>();
		return source.Filter((T it) => seen.Add(selector(it)));
	}

	public static IPersistentStableMap<TKey, IPersistentStableList<T>> GroupBy<T, TKey>(this IStableCollection<T> source, Func<T, TKey> keySelector)
	{
		return source.GroupBy(keySelector, (T it) => it);
	}

	public static IPersistentStableMap<TKey, IPersistentStableList<TValue>> GroupBy<T, TKey, TValue>(this IStableCollection<T> source, Func<T, TKey> keySelector, Func<T, TValue> valueTransform)
	{
		Dictionary<TKey, List<TValue>> groups = new Dictionary<TKey, List<TValue>>();
		List<TKey> list = new List<TKey>();
		foreach (T item in source)
		{
			TKey val = keySelector(item);
			if (!groups.TryGetValue(val, out List<TValue> value))
			{
				value = new List<TValue>();
				groups[val] = value;
				list.Add(val);
			}
			value.Add(valueTransform(item));
		}
		return list.Select((TKey key) => (key: key, groups[key].ToPersistentStableList())).ToPersistentStableMap();
	}

	public static IPersistentStableList<(T First, R Second)> Zip<T, R>(this IStableCollection<T> source, IEnumerable<R> other)
	{
		return source.Zip(other, (T a, R b) => (a: a, b: b));
	}

	public static IPersistentStableList<V> Zip<T, R, V>(this IStableCollection<T> source, IEnumerable<R> other, Func<T, R, V> transform)
	{
		return Enumerable.Zip(source, other, transform).ToPersistentStableList();
	}

	public static IPersistentStableSet<T> Union<T>(this IStableCollection<T> source, IEnumerable<T> other)
	{
		HashSet<T> hashSet = source.ToHashSet();
		hashSet.UnionWith(other);
		return hashSet.ToPersistentStableSet();
	}

	public static IPersistentStableSet<T> Intersect<T>(this IStableCollection<T> source, IEnumerable<T> other)
	{
		HashSet<T> hashSet = source.ToHashSet();
		hashSet.IntersectWith(other);
		return hashSet.ToPersistentStableSet();
	}

	public static IStableCollection<T> IfEmpty<T>(this IStableCollection<T> source, Func<IStableCollection<T>> defaultValue)
	{
		if (!source.IsEmpty())
		{
			return source;
		}
		return defaultValue();
	}

	public static IPersistentStableSet<T> Plus<T>(this IStableSet<T> source, T element)
	{
		HashSet<T> hashSet = source.ToHashSet();
		hashSet.Add(element);
		return hashSet.ToPersistentStableSet();
	}

	public static IPersistentStableSet<T> Plus<T>(this IStableSet<T> source, IEnumerable<T> elements)
	{
		return source.Union(elements);
	}

	public static IPersistentStableSet<T> Minus<T>(this IStableSet<T> source, T element)
	{
		HashSet<T> hashSet = source.ToHashSet();
		hashSet.Remove(element);
		return hashSet.ToPersistentStableSet();
	}

	public static IPersistentStableSet<T> Minus<T>(this IStableSet<T> source, IEnumerable<T> elements)
	{
		return source.Subtract(elements);
	}

	public static IPersistentStableList<T> Plus<T>(this IStableList<T> source, T element)
	{
		return StableEnumerableExtensions.Plus(source, element);
	}

	public static IPersistentStableList<T> Plus<T>(this IStableList<T> source, IEnumerable<T> elements)
	{
		return StableEnumerableExtensions.Plus(source, elements);
	}

	public static IPersistentStableList<T> Minus<T>(this IStableList<T> source, T element)
	{
		return StableEnumerableExtensions.Minus(source, element);
	}

	public static IPersistentStableList<T> Minus<T>(this IStableList<T> source, IEnumerable<T> elements)
	{
		return StableEnumerableExtensions.Minus(source, elements);
	}
}

using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public static class StableNullableExtensions
{
	public static T? FirstOrNull<T>(this IEnumerable<T> source) where T : class?
	{
		if (!source.TryFind((T _) => true, out var value))
		{
			return null;
		}
		return value;
	}

	public static T? FirstOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : class?
	{
		if (!source.TryFind(predicate, out var value))
		{
			return null;
		}
		return value;
	}

	public static T? Find<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : class?
	{
		return FirstOrNull(source, predicate);
	}

	public static T? LastOrNull<T>(this IEnumerable<T> source) where T : class?
	{
		if (!source.TryFindLast((T _) => true, out var value))
		{
			return null;
		}
		return value;
	}

	public static T? LastOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : class?
	{
		if (!source.TryFindLast(predicate, out var value))
		{
			return null;
		}
		return value;
	}

	public static T? FindLast<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : class?
	{
		return LastOrNull(source, predicate);
	}

	public static T? SingleOrNull<T>(this IEnumerable<T> source) where T : class?
	{
		if (!source.TrySingle((T _) => true, out var value))
		{
			return null;
		}
		return value;
	}

	public static T? SingleOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : class?
	{
		if (!source.TrySingle(predicate, out var value))
		{
			return null;
		}
		return value;
	}

	public static T? GetOrNull<T>(this IStableList<T> list, int index) where T : class?
	{
		if (index < 0 || index >= list.Size)
		{
			return null;
		}
		return list[index];
	}

	public static T? ElementAtOrNull<T>(this IEnumerable<T> source, int index) where T : class?
	{
		if (!source.TryElementAt(index, out var value))
		{
			return null;
		}
		return value;
	}

	public static T? RandomOrNull<T>(this IEnumerable<T> source, Random? random = null) where T : class?
	{
		if (!source.None())
		{
			return source.Random(random);
		}
		return null;
	}

	public static T? MaxOrNull<T>(this IEnumerable<T> source) where T : class?
	{
		if (!source.TryExtreme(Comparer<T>.Default, max: true, out var result))
		{
			return null;
		}
		return result;
	}

	public static T? MinOrNull<T>(this IEnumerable<T> source) where T : class?
	{
		if (!source.TryExtreme(Comparer<T>.Default, max: false, out var result))
		{
			return null;
		}
		return result;
	}

	public static T? MaxWithOrNull<T>(this IEnumerable<T> source, IComparer<T> comparer) where T : class?
	{
		if (!source.TryExtreme(comparer, max: true, out var result))
		{
			return null;
		}
		return result;
	}

	public static T? MinWithOrNull<T>(this IEnumerable<T> source, IComparer<T> comparer) where T : class?
	{
		if (!source.TryExtreme(comparer, max: false, out var result))
		{
			return null;
		}
		return result;
	}

	public static T? MaxByOrNull<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where T : class?
	{
		if (!source.TryExtremeBy(selector, max: true, out var value))
		{
			return null;
		}
		return value;
	}

	public static T? MinByOrNull<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where T : class?
	{
		if (!source.TryExtremeBy(selector, max: false, out var value))
		{
			return null;
		}
		return value;
	}

	public static R? MaxOfOrNull<T, R>(this IEnumerable<T> source, Func<T, R> selector) where R : class?
	{
		if (!source.None())
		{
			return source.MaxOf(selector);
		}
		return null;
	}

	public static R? MinOfOrNull<T, R>(this IEnumerable<T> source, Func<T, R> selector) where R : class?
	{
		if (!source.None())
		{
			return source.MinOf(selector);
		}
		return null;
	}

	public static T? ReduceOrNull<T>(this IEnumerable<T> source, Func<T, T, T> operation) where T : class?
	{
		if (!source.None())
		{
			return source.Reduce(operation);
		}
		return null;
	}

	public static IPersistentStableList<T> FilterNotNull<T>(this IEnumerable<T?> source) where T : class
	{
		List<T> list = new List<T>();
		foreach (T item in source)
		{
			if (item != null)
			{
				list.Add(item);
			}
		}
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<R> MapNotNull<T, R>(this IEnumerable<T> source, Func<T, R?> transform) where R : class
	{
		List<R> list = new List<R>();
		foreach (T item in source)
		{
			R val = transform(item);
			if (val != null)
			{
				list.Add(val);
			}
		}
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<R> MapIndexedNotNull<T, R>(this IEnumerable<T> source, Func<int, T, R?> transform) where R : class
	{
		List<R> list = new List<R>();
		int num = 0;
		foreach (T item in source)
		{
			R val = transform(num++, item);
			if (val != null)
			{
				list.Add(val);
			}
		}
		return list.ToPersistentStableList();
	}

	public static R? FirstNotNullOfOrNull<T, R>(this IEnumerable<T> source, Func<T, R?> transform) where R : class
	{
		foreach (T item in source)
		{
			R val = transform(item);
			if (val != null)
			{
				return val;
			}
		}
		return null;
	}

	public static R FirstNotNullOf<T, R>(this IEnumerable<T> source, Func<T, R?> transform) where R : class
	{
		return source.FirstNotNullOfOrNull(transform) ?? throw new InvalidOperationException("No element of the collection was transformed to a non-null value.");
	}

	public static TValue? GetOrNull<TKey, TValue>(this IStableMap<TKey, TValue> map, TKey key) where TValue : class?
	{
		if (!map.ContainsKey(key))
		{
			return null;
		}
		return map[key];
	}

	public static T? RemoveFirstOrNull<T>(this IMutableStableList<T> list) where T : class?
	{
		if (!list.IsEmpty())
		{
			return list.RemoveFirst();
		}
		return null;
	}

	public static T? RemoveLastOrNull<T>(this IMutableStableList<T> list) where T : class?
	{
		if (!list.IsEmpty())
		{
			return list.RemoveLast();
		}
		return null;
	}

	internal static bool TryFind<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T value)
	{
		foreach (T item in source)
		{
			if (predicate(item))
			{
				value = item;
				return true;
			}
		}
		value = default;
		return false;
	}

	internal static bool TryFindLast<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T value)
	{
		if (source is IStableList<T> stableList)
		{
			for (int num = stableList.Size - 1; num >= 0; num--)
			{
				if (predicate(stableList[num]))
				{
					value = stableList[num];
					return true;
				}
			}
			value = default;
			return false;
		}
		bool result = false;
		value = default;
		foreach (T item in source)
		{
			if (predicate(item))
			{
				value = item;
				result = true;
			}
		}
		return result;
	}

	internal static bool TrySingle<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T value)
	{
		bool flag = false;
		value = default;
		foreach (T item in source)
		{
			if (predicate(item))
			{
				if (flag)
				{
					value = default;
					return false;
				}
				value = item;
				flag = true;
			}
		}
		return flag;
	}

	internal static bool TryExtremeBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, bool max, out T value)
	{
		Comparer<TKey> comparer = Comparer<TKey>.Default;
		using IEnumerator<T> enumerator = source.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			value = default;
			return false;
		}
		value = enumerator.Current;
		TKey y = selector(value);
		while (enumerator.MoveNext())
		{
			TKey val = selector(enumerator.Current);
			int num = comparer.Compare(val, y);
			if (max ? (num > 0) : (num < 0))
			{
				value = enumerator.Current;
				y = val;
			}
		}
		return true;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StableCollections.Map.Persistent;
using StableCollections.Set.Mutable;
using StableCollections.Set.Persistent;

namespace StableCollections;

public static class StableEnumerableExtensions
{
	public static IPersistentStableList<R> Map<T, R>(this IEnumerable<T> source, Func<T, R> transform)
	{
		List<R> list = new List<R>(source.SizeHint());
		foreach (T item in source)
		{
			list.Add(transform(item));
		}
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<R> MapIndexed<T, R>(this IEnumerable<T> source, Func<int, T, R> transform)
	{
		List<R> list = new List<R>(source.SizeHint());
		int num = 0;
		foreach (T item in source)
		{
			list.Add(transform(num++, item));
		}
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<R> FlatMap<T, R>(this IEnumerable<T> source, Func<T, IEnumerable<R>> transform)
	{
		List<R> list = new List<R>();
		foreach (T item in source)
		{
			list.AddRange(transform(item));
		}
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<R> FlatMapIndexed<T, R>(this IEnumerable<T> source, Func<int, T, IEnumerable<R>> transform)
	{
		List<R> list = new List<R>();
		int num = 0;
		foreach (T item in source)
		{
			list.AddRange(transform(num++, item));
		}
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
	{
		List<T> list = new List<T>();
		foreach (IEnumerable<T> item in source)
		{
			list.AddRange(item);
		}
		return list.ToPersistentStableList();
	}

	public static IEnumerable<(int Index, T Value)> WithIndex<T>(this IEnumerable<T> source)
	{
		int index = 0;
		foreach (T item in source)
		{
			yield return (Index: index++, Value: item);
		}
	}

	public static IPersistentStableList<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		List<T> list = new List<T>();
		foreach (T item in source)
		{
			if (predicate(item))
			{
				list.Add(item);
			}
		}
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<T> FilterIndexed<T>(this IEnumerable<T> source, Func<int, T, bool> predicate)
	{
		List<T> list = new List<T>();
		int num = 0;
		foreach (T item in source)
		{
			if (predicate(num++, item))
			{
				list.Add(item);
			}
		}
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<T> FilterNot<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		return source.Filter((T it) => !predicate(it));
	}

	public static IPersistentStableList<R> FilterIsInstance<R>(this IEnumerable source)
	{
		List<R> list = new List<R>();
		foreach (object item2 in source)
		{
			if (item2 is R item)
			{
				list.Add(item);
			}
		}
		return list.ToPersistentStableList();
	}

	public static (IPersistentStableList<T> Matching, IPersistentStableList<T> NotMatching) Partition<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		List<T> list = new List<T>();
		List<T> list2 = new List<T>();
		foreach (T item in source)
		{
			if (predicate(item))
			{
				list.Add(item);
			}
			else
			{
				list2.Add(item);
			}
		}
		return (Matching: list.ToPersistentStableList(), NotMatching: list2.ToPersistentStableList());
	}

	public static IPersistentStableList<T> Drop<T>(this IEnumerable<T> source, int n)
	{
		RequireNonNegative(n, "n");
		return source.Skip(n).ToPersistentStableList();
	}

	public static IPersistentStableList<T> DropWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		return source.SkipWhile(predicate).ToPersistentStableList();
	}

	public static IPersistentStableMap<TKey, TValue> Associate<T, TKey, TValue>(this IEnumerable<T> source, Func<T, (TKey Key, TValue Value)> transform)
	{
		return source.Select(transform).ToPersistentStableMap();
	}

	public static IPersistentStableMap<TKey, T> AssociateBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
	{
		return source.Select((T it) => (keySelector(it), it: it)).ToPersistentStableMap();
	}

	public static IPersistentStableMap<TKey, TValue> AssociateBy<T, TKey, TValue>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TValue> valueTransform)
	{
		return source.Select((T it) => (keySelector(it), valueTransform(it))).ToPersistentStableMap();
	}

	public static IPersistentStableMap<T, TValue> AssociateWith<T, TValue>(this IEnumerable<T> source, Func<T, TValue> valueSelector)
	{
		return source.Select((T it) => (it: it, valueSelector(it))).ToPersistentStableMap();
	}

	public static IPersistentStableMap<TKey, int> EachCount<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
	{
		Dictionary<TKey, int> dictionary = new Dictionary<TKey, int>();
		foreach (T item in source)
		{
			TKey key = keySelector(item);
			dictionary[key] = ((!dictionary.TryGetValue(key, out var value)) ? 1 : (value + 1));
		}
		return dictionary.Select((KeyValuePair<TKey, int> it) => (Key: it.Key, Value: it.Value)).ToPersistentStableMap();
	}

	public static IPersistentStableList<IPersistentStableList<T>> Chunked<T>(this IEnumerable<T> source, int size)
	{
		return source.Windowed(size, size, partialWindows: true);
	}

	public static IPersistentStableList<R> Chunked<T, R>(this IEnumerable<T> source, int size, Func<IPersistentStableList<T>, R> transform)
	{
		return source.Chunked(size).Map(transform);
	}

	public static IPersistentStableList<IPersistentStableList<T>> Windowed<T>(this IEnumerable<T> source, int size, int step = 1, bool partialWindows = false)
	{
		if (size <= 0 || step <= 0)
		{
			throw new ArgumentException($"size {size} and step {step} must be greater than zero.");
		}
		IList<T> list = (source as IList<T>) ?? source.ToList();
		List<IPersistentStableList<T>> list2 = new List<IPersistentStableList<T>>();
		for (int i = 0; i < list.Count; i += step)
		{
			int num = Math.Min(size, list.Count - i);
			if (num < size && !partialWindows)
			{
				break;
			}
			List<T> list3 = new List<T>(num);
			for (int j = 0; j < num; j++)
			{
				list3.Add(list[i + j]);
			}
			list2.Add(list3.ToPersistentStableList());
		}
		return list2.ToPersistentStableList();
	}

	public static IPersistentStableList<R> Windowed<T, R>(this IEnumerable<T> source, int size, int step, bool partialWindows, Func<IPersistentStableList<T>, R> transform)
	{
		return source.Windowed(size, step, partialWindows).Map(transform);
	}

	public static IPersistentStableList<(T First, T Second)> ZipWithNext<T>(this IEnumerable<T> source)
	{
		return source.ZipWithNext((T a, T b) => (a: a, b: b));
	}

	public static IPersistentStableList<R> ZipWithNext<T, R>(this IEnumerable<T> source, Func<T, T, R> transform)
	{
		List<R> list = new List<R>();
		using IEnumerator<T> enumerator = source.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return list.ToPersistentStableList();
		}
		T arg = enumerator.Current;
		while (enumerator.MoveNext())
		{
			T current = enumerator.Current;
			list.Add(transform(arg, current));
			arg = current;
		}
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<T> Sorted<T>(this IEnumerable<T> source)
	{
		return source.SortedWith(Comparer<T>.Default);
	}

	public static IPersistentStableList<T> SortedDescending<T>(this IEnumerable<T> source)
	{
		return source.SortedWith((T a, T b) => Comparer<T>.Default.Compare(b, a));
	}

	public static IPersistentStableList<T> SortedBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
	{
		return source.OrderBy(selector).ToPersistentStableList();
	}

	public static IPersistentStableList<T> SortedByDescending<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
	{
		return source.OrderByDescending(selector).ToPersistentStableList();
	}

	public static IPersistentStableList<T> SortedWith<T>(this IEnumerable<T> source, IComparer<T> comparer)
	{
		return source.OrderBy((T it) => it, comparer).ToPersistentStableList();
	}

	public static IPersistentStableList<T> SortedWith<T>(this IEnumerable<T> source, Comparison<T> comparison)
	{
		return source.SortedWith(Comparer<T>.Create(comparison));
	}

	public static IPersistentStableList<T> Reversed<T>(this IEnumerable<T> source)
	{
		List<T> list = source.ToList();
		list.Reverse();
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<T> Shuffled<T>(this IEnumerable<T> source, Random? random = null)
	{
		List<T> list = source.ToList();
		list.ShuffleInPlace(random ?? KotlinRandom.Instance);
		return list.ToPersistentStableList();
	}

	public static TAccumulate Fold<T, TAccumulate>(this IEnumerable<T> source, TAccumulate initial, Func<TAccumulate, T, TAccumulate> operation)
	{
		TAccumulate val = initial;
		foreach (T item in source)
		{
			val = operation(val, item);
		}
		return val;
	}

	public static TAccumulate FoldIndexed<T, TAccumulate>(this IEnumerable<T> source, TAccumulate initial, Func<int, TAccumulate, T, TAccumulate> operation)
	{
		TAccumulate val = initial;
		int num = 0;
		foreach (T item in source)
		{
			val = operation(num++, val, item);
		}
		return val;
	}

	public static T Reduce<T>(this IEnumerable<T> source, Func<T, T, T> operation)
	{
		using IEnumerator<T> enumerator = source.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			throw new InvalidOperationException("Empty collection can't be reduced.");
		}
		T val = enumerator.Current;
		while (enumerator.MoveNext())
		{
			val = operation(val, enumerator.Current);
		}
		return val;
	}

	public static T ReduceIndexed<T>(this IEnumerable<T> source, Func<int, T, T, T> operation)
	{
		using IEnumerator<T> enumerator = source.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			throw new InvalidOperationException("Empty collection can't be reduced.");
		}
		T val = enumerator.Current;
		int num = 1;
		while (enumerator.MoveNext())
		{
			val = operation(num++, val, enumerator.Current);
		}
		return val;
	}

	public static IPersistentStableList<TAccumulate> RunningFold<T, TAccumulate>(this IEnumerable<T> source, TAccumulate initial, Func<TAccumulate, T, TAccumulate> operation)
	{
		List<TAccumulate> list = new List<TAccumulate> { initial };
		TAccumulate val = initial;
		foreach (T item in source)
		{
			val = operation(val, item);
			list.Add(val);
		}
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<TAccumulate> Scan<T, TAccumulate>(this IEnumerable<T> source, TAccumulate initial, Func<TAccumulate, T, TAccumulate> operation)
	{
		return source.RunningFold(initial, operation);
	}

	public static IPersistentStableList<T> RunningReduce<T>(this IEnumerable<T> source, Func<T, T, T> operation)
	{
		List<T> list = new List<T>();
		using IEnumerator<T> enumerator = source.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return list.ToPersistentStableList();
		}
		T val = enumerator.Current;
		list.Add(val);
		while (enumerator.MoveNext())
		{
			val = operation(val, enumerator.Current);
			list.Add(val);
		}
		return list.ToPersistentStableList();
	}

	public static int SumOf<T>(this IEnumerable<T> source, Func<T, int> selector)
	{
		return source.Sum(selector);
	}

	public static long SumOf<T>(this IEnumerable<T> source, Func<T, long> selector)
	{
		return source.Sum(selector);
	}

	public static float SumOf<T>(this IEnumerable<T> source, Func<T, float> selector)
	{
		return source.Sum(selector);
	}

	public static double SumOf<T>(this IEnumerable<T> source, Func<T, double> selector)
	{
		return source.Sum(selector);
	}

	public static decimal SumOf<T>(this IEnumerable<T> source, Func<T, decimal> selector)
	{
		return source.Sum(selector);
	}

	public static R MaxOf<T, R>(this IEnumerable<T> source, Func<T, R> selector)
	{
		return source.Select(selector).ExtremeOrThrow(Comparer<R>.Default, max: true);
	}

	public static R MinOf<T, R>(this IEnumerable<T> source, Func<T, R> selector)
	{
		return source.Select(selector).ExtremeOrThrow(Comparer<R>.Default, max: false);
	}

	public static T MaxWith<T>(this IEnumerable<T> source, IComparer<T> comparer)
	{
		return source.ExtremeOrThrow(comparer, max: true);
	}

	public static T MinWith<T>(this IEnumerable<T> source, IComparer<T> comparer)
	{
		return source.ExtremeOrThrow(comparer, max: false);
	}

	public static bool None<T>(this IEnumerable<T> source)
	{
		return !source.Any();
	}

	public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		return !source.Any(predicate);
	}

	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (T item in source)
		{
			action(item);
		}
	}

	public static void ForEachIndexed<T>(this IEnumerable<T> source, Action<int, T> action)
	{
		int num = 0;
		foreach (T item in source)
		{
			action(num++, item);
		}
	}

	public static IEnumerable<T> OnEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		source.ForEach(action);
		return source;
	}

	public static IPersistentStableList<T> OnEach<T>(this IPersistentStableList<T> source, Action<T> action)
	{
		source.ForEach(action);
		return source;
	}

	public static IMutableStableList<T> OnEach<T>(this IMutableStableList<T> source, Action<T> action)
	{
		source.ForEach(action);
		return source;
	}

	public static IPersistentStableSet<T> OnEach<T>(this IPersistentStableSet<T> source, Action<T> action)
	{
		source.ForEach(action);
		return source;
	}

	public static IMutableStableSet<T> OnEach<T>(this IMutableStableSet<T> source, Action<T> action)
	{
		source.ForEach(action);
		return source;
	}

	public static int IndexOfFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		int num = 0;
		foreach (T item in source)
		{
			if (predicate(item))
			{
				return num;
			}
			num++;
		}
		return -1;
	}

	public static int IndexOfLast<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		int result = -1;
		int num = 0;
		foreach (T item in source)
		{
			if (predicate(item))
			{
				result = num;
			}
			num++;
		}
		return result;
	}

	public static T ElementAtOrElse<T>(this IEnumerable<T> source, int index, Func<int, T> defaultValue)
	{
		if (!source.TryElementAt(index, out var value))
		{
			return defaultValue(index);
		}
		return value;
	}

	public static T Random<T>(this IEnumerable<T> source, Random? random = null)
	{
		IList<T> list = (source as IList<T>) ?? source.ToList();
		if (list.Count == 0)
		{
			throw new InvalidOperationException("Collection is empty.");
		}
		return list[(random ?? KotlinRandom.Instance).Next(list.Count)];
	}

	public static IPersistentStableSet<T> Subtract<T>(this IEnumerable<T> source, IEnumerable<T> other)
	{
		HashSet<T> hashSet = source.ToHashSet();
		hashSet.ExceptWith(other);
		return hashSet.ToPersistentStableSet();
	}

	public static IPersistentStableSet<T> ToSet<T>(this IEnumerable<T> source)
	{
		return source.ToPersistentStableSet();
	}

	public static IPersistentStableList<T> Plus<T>(this IEnumerable<T> source, T element)
	{
		List<T> list = source.ToList();
		list.Add(element);
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<T> Plus<T>(this IEnumerable<T> source, IEnumerable<T> elements)
	{
		List<T> list = source.ToList();
		list.AddRange(elements);
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<T> Minus<T>(this IEnumerable<T> source, T element)
	{
		List<T> list = source.ToList();
		list.Remove(element);
		return list.ToPersistentStableList();
	}

	public static IPersistentStableList<T> Minus<T>(this IEnumerable<T> source, IEnumerable<T> elements)
	{
		HashSet<T> excluded = elements.ToHashSet();
		return source.Filter((T it) => !excluded.Contains(it));
	}

	public static string JoinToString<T>(this IEnumerable<T> source, string separator = ", ", string prefix = "", string postfix = "", int limit = -1, string truncated = "...", Func<T, string>? transform = null)
	{
		StringBuilder stringBuilder = new StringBuilder(prefix);
		int num = 0;
		foreach (T item in source)
		{
			if (++num > 1)
			{
				stringBuilder.Append(separator);
			}
			if (limit >= 0 && num > limit)
			{
				stringBuilder.Append(truncated);
				break;
			}
			stringBuilder.Append((transform != null) ? transform(item) : Utils.ToString(item));
		}
		stringBuilder.Append(postfix);
		return stringBuilder.ToString();
	}

	internal static int SizeHint<T>(this IEnumerable<T> source)
	{
		if (!(source is ICollection<T> { Count: var count }))
		{
			if (!(source is IStableCollection<T> { Size: var size }))
			{
				return 0;
			}
			return size;
		}
		return count;
	}

	internal static bool TryElementAt<T>(this IEnumerable<T> source, int index, out T value)
	{
		if (index >= 0)
		{
			if (source is IList<T> list)
			{
				if (index < list.Count)
				{
					value = list[index];
					return true;
				}
			}
			else if (source is IStableList<T> stableList)
			{
				if (index < stableList.Size)
				{
					value = stableList[index];
					return true;
				}
			}
			else
			{
				int num = 0;
				foreach (T item in source)
				{
					if (num++ == index)
					{
						value = item;
						return true;
					}
				}
			}
		}
		value = default;
		return false;
	}

	internal static bool TryExtreme<T>(this IEnumerable<T> source, IComparer<T> comparer, bool max, out T result)
	{
		using IEnumerator<T> enumerator = source.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			result = default;
			return false;
		}
		result = enumerator.Current;
		while (enumerator.MoveNext())
		{
			int num = comparer.Compare(enumerator.Current, result);
			if (max ? (num > 0) : (num < 0))
			{
				result = enumerator.Current;
			}
		}
		return true;
	}

	private static T ExtremeOrThrow<T>(this IEnumerable<T> source, IComparer<T> comparer, bool max)
	{
		if (!source.TryExtreme(comparer, max, out var result))
		{
			throw new InvalidOperationException("Collection is empty.");
		}
		return result;
	}

	internal static void ShuffleInPlace<T>(this IList<T> list, Random random)
	{
		for (int num = list.Count - 1; num > 0; num--)
		{
			int num2 = random.Next(num + 1);
			int index = num;
			int index2 = num2;
			T value = list[num2];
			T value2 = list[num];
			list[index] = value;
			list[index2] = value2;
		}
	}

	internal static void RequireNonNegative(int n, string name)
	{
		if (n < 0)
		{
			throw new ArgumentException($"Requested element count {n} is less than zero.", name);
		}
	}

	public static IMutableStableList<T> ToMutableStableList<T>(this IEnumerable<T> enumerable)
	{
		return new MutableStableListImpl<T>(enumerable);
	}

	public static IPersistentStableList<T> ToPersistentStableList<T>(this IEnumerable<T> enumerable)
	{
		return new PersistentStableListImpl<T>(enumerable);
	}

	public static IMutableStableMap<TKey, TValue> ToMutableStableMap<TKey, TValue>(this IEnumerable<StableMapEntry<TKey, TValue>> enumerable)
	{
		return new MutableStableMapImpl<TKey, TValue>(enumerable);
	}

	public static IMutableStableMap<TKey, TValue> ToMutableStableMap<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> enumerable)
	{
		return new MutableStableMapImpl<TKey, TValue>(enumerable);
	}

	public static IPersistentStableMap<TKey, TValue> ToPersistentStableMap<TKey, TValue>(this IEnumerable<StableMapEntry<TKey, TValue>> enumerable)
	{
		return new PersistentStableMapImpl<TKey, TValue>(enumerable);
	}

	public static IPersistentStableMap<TKey, TValue> ToPersistentStableMap<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> enumerable)
	{
		return new PersistentStableMapImpl<TKey, TValue>(enumerable);
	}

	public static IMutableStableSet<T> ToMutableStableSet<T>(this IEnumerable<T> enumerable)
	{
		return new MutableStableSetImpl<T>(enumerable);
	}

	public static IPersistentStableSet<T> ToPersistentStableSet<T>(this IEnumerable<T> enumerable)
	{
		return new PersistentStableSetImpl<T>(enumerable);
	}
}

using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public static class StableListExtensions
{
	public static T GetOrElse<T>(this IStableList<T> list, int index, Func<int, T> defaultValue)
	{
		if (index < 0 || index >= list.Size)
		{
			return defaultValue(index);
		}
		return list[index];
	}

	public static IPersistentStableList<T> Slice<T>(this IStableList<T> list, int fromInclusive, int toInclusive)
	{
		if (fromInclusive > toInclusive)
		{
			return IPersistentStableList.Empty<T>();
		}
		return list.SubList(fromInclusive, toInclusive + 1).ToPersistentStableList();
	}

	public static IPersistentStableList<T> Slice<T>(this IStableList<T> list, IEnumerable<int> indices)
	{
		return indices.Map((int i) => list[i]);
	}

	public static IPersistentStableList<T> TakeLast<T>(this IStableList<T> list, int n)
	{
		StableEnumerableExtensions.RequireNonNegative(n, "n");
		int count = Math.Min(n, list.Size);
		return PersistentStableList(count, (int i) => list[list.Size - count + i]);
	}

	public static IPersistentStableList<T> TakeLastWhile<T>(this IStableList<T> list, Func<T, bool> predicate)
	{
		int num = list.Size;
		while (num > 0 && predicate(list[num - 1]))
		{
			num--;
		}
		return list.Drop(num);
	}

	public static IPersistentStableList<T> DropLast<T>(this IStableList<T> list, int n)
	{
		StableEnumerableExtensions.RequireNonNegative(n, "n");
		return list.Take(Math.Max(list.Size - n, 0));
	}

	public static IPersistentStableList<T> DropLastWhile<T>(this IStableList<T> list, Func<T, bool> predicate)
	{
		int num = list.Size;
		while (num > 0 && predicate(list[num - 1]))
		{
			num--;
		}
		return list.Take(num);
	}

	public static TAccumulate FoldRight<T, TAccumulate>(this IStableList<T> list, TAccumulate initial, Func<T, TAccumulate, TAccumulate> operation)
	{
		TAccumulate val = initial;
		for (int num = list.Size - 1; num >= 0; num--)
		{
			val = operation(list[num], val);
		}
		return val;
	}

	public static TAccumulate FoldRightIndexed<T, TAccumulate>(this IStableList<T> list, TAccumulate initial, Func<int, T, TAccumulate, TAccumulate> operation)
	{
		TAccumulate val = initial;
		for (int num = list.Size - 1; num >= 0; num--)
		{
			val = operation(num, list[num], val);
		}
		return val;
	}

	public static T ReduceRight<T>(this IStableList<T> list, Func<T, T, T> operation)
	{
		if (list.IsEmpty())
		{
			throw new InvalidOperationException("Empty list can't be reduced.");
		}
		T val = list[list.Size - 1];
		for (int num = list.Size - 2; num >= 0; num--)
		{
			val = operation(list[num], val);
		}
		return val;
	}

	public static int BinarySearch<T>(this IStableList<T> list, T element, IComparer<T>? comparer = null, int fromIndex = 0, int? toIndex = null)
	{
		if (comparer == null)
		{
			comparer = Comparer<T>.Default;
		}
		int num = fromIndex;
		int num2 = (toIndex ?? list.Size) - 1;
		while (num <= num2)
		{
			int num3 = num + num2 >>> 1;
			int num4 = comparer.Compare(list[num3], element);
			if (num4 < 0)
			{
				num = num3 + 1;
				continue;
			}
			if (num4 > 0)
			{
				num2 = num3 - 1;
				continue;
			}
			return num3;
		}
		return -(num + 1);
	}

	public static int BinarySearchBy<T, TKey>(this IStableList<T> list, TKey key, Func<T, TKey> selector)
	{
		Comparer<TKey> comparer = Comparer<TKey>.Default;
		int num = 0;
		int num2 = list.Size - 1;
		while (num <= num2)
		{
			int num3 = num + num2 >>> 1;
			int num4 = comparer.Compare(selector(list[num3]), key);
			if (num4 < 0)
			{
				num = num3 + 1;
				continue;
			}
			if (num4 > 0)
			{
				num2 = num3 - 1;
				continue;
			}
			return num3;
		}
		return -(num + 1);
	}

	public static void Deconstruct<T>(this IStableList<T> list, out T first, out T second)
	{
		first = list[0];
		second = list[1];
	}

	public static void Deconstruct<T>(this IStableList<T> list, out T first, out T second, out T third)
	{
		list.Deconstruct(out first, out second);
		third = list[2];
	}

	public static void Deconstruct<T>(this IStableList<T> list, out T first, out T second, out T third, out T fourth)
	{
		list.Deconstruct(out first, out second, out third);
		fourth = list[3];
	}

	public static void Deconstruct<T>(this IStableList<T> list, out T first, out T second, out T third, out T fourth, out T fifth)
	{
		list.Deconstruct(out first, out second, out third, out fourth);
		fifth = list[4];
	}

	private static IPersistentStableList<T> PersistentStableList<T>(int count, Func<int, T> init)
	{
		return StableCollectionsFactory.PersistentStableList(count, init);
	}
}

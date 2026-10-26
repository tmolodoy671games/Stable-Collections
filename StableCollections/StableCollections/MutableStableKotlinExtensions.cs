using System;
using System.Collections.Generic;

namespace StableCollections;

public static class MutableStableKotlinExtensions
{
	public static bool RemoveAll<T>(this IMutableStableCollection<T> collection, Func<T, bool> predicate)
	{
		bool result = false;
		foreach (T item in collection.Filter(predicate))
		{
			if (collection.Remove(item))
			{
				result = true;
			}
		}
		return result;
	}

	public static bool RetainAll<T>(this IMutableStableCollection<T> collection, Func<T, bool> predicate)
	{
		return collection.RemoveAll((T it) => !predicate(it));
	}

	public static bool RemoveAll<T>(this IMutableStableList<T> list, Func<T, bool> predicate)
	{
		IPersistentStableList<T> persistentStableList = list.FilterNot(predicate);
		if (persistentStableList.Size == list.Size)
		{
			return false;
		}
		list.ReplaceWith(persistentStableList);
		return true;
	}

	public static bool RetainAll<T>(this IMutableStableList<T> list, Func<T, bool> predicate)
	{
		return list.RemoveAll((T it) => !predicate(it));
	}

	public static T RemoveFirst<T>(this IMutableStableList<T> list)
	{
		if (list.IsEmpty())
		{
			throw new InvalidOperationException("List is empty.");
		}
		T result = list[0];
		list.RemoveAt(0);
		return result;
	}

	public static T RemoveLast<T>(this IMutableStableList<T> list)
	{
		if (list.IsEmpty())
		{
			throw new InvalidOperationException("List is empty.");
		}
		T result = list[list.Size - 1];
		list.RemoveAt(list.Size - 1);
		return result;
	}

	public static void AddFirst<T>(this IMutableStableList<T> list, T element)
	{
		list.Add(0, element);
	}

	public static void AddLast<T>(this IMutableStableList<T> list, T element)
	{
		list.Add(element);
	}

	public static void Sort<T>(this IMutableStableList<T> list)
	{
		list.SortWith(Comparer<T>.Default);
	}

	public static void SortDescending<T>(this IMutableStableList<T> list)
	{
		list.SortWith((T a, T b) => Comparer<T>.Default.Compare(b, a));
	}

	public static void SortBy<T, TKey>(this IMutableStableList<T> list, Func<T, TKey> selector)
	{
		list.ReplaceWith(list.SortedBy(selector));
	}

	public static void SortByDescending<T, TKey>(this IMutableStableList<T> list, Func<T, TKey> selector)
	{
		list.ReplaceWith(list.SortedByDescending(selector));
	}

	public static void SortWith<T>(this IMutableStableList<T> list, IComparer<T> comparer)
	{
		list.ReplaceWith(list.SortedWith(comparer));
	}

	public static void SortWith<T>(this IMutableStableList<T> list, Comparison<T> comparison)
	{
		list.ReplaceWith(list.SortedWith(comparison));
	}

	public static void Reverse<T>(this IMutableStableList<T> list)
	{
		int num = 0;
		int num2 = list.Size - 1;
		while (num < num2)
		{
			int index = num;
			int index2 = num2;
			T value = list[num2];
			T value2 = list[num];
			list[index] = value;
			list[index2] = value2;
			num++;
			num2--;
		}
	}

	public static void Shuffle<T>(this IMutableStableList<T> list, Random? random = null)
	{
		if (random == null)
		{
			random = KotlinRandom.Instance;
		}
		for (int num = list.Size - 1; num > 0; num--)
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

	public static void Fill<T>(this IMutableStableList<T> list, T value)
	{
		for (int i = 0; i < list.Size; i++)
		{
			list[i] = value;
		}
	}

	private static void ReplaceWith<T>(this IMutableStableList<T> list, IStableList<T> elements)
	{
		int size = list.Size;
		for (int i = 0; i < elements.Size; i++)
		{
			list[i] = elements[i];
		}
		for (int num = size - 1; num >= elements.Size; num--)
		{
			list.RemoveAt(num);
		}
	}
}

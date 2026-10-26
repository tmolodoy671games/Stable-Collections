using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public static class MutableStableCollectionExtensions
{
	public static bool AddAll<T>(this IMutableStableCollection<T> stableCollection, IEnumerable<T> elements)
	{
		if (elements is IStableCollection<T> elements2)
		{
			return stableCollection.AddAll(elements2);
		}
		bool result = false;
		foreach (T element in elements)
		{
			if (stableCollection.Add(element))
			{
				result = true;
			}
		}
		return result;
	}

	public static bool AddAll<T>(this IMutableStableCollection<T> stableCollection, ReadOnlySpan<T> elements)
	{
		bool result = false;
		ReadOnlySpan<T> readOnlySpan = elements;
		for (int i = 0; i < readOnlySpan.Length; i++)
		{
			T element = readOnlySpan[i];
			if (stableCollection.Add(element))
			{
				result = true;
			}
		}
		return result;
	}

	public static bool AddAll<T>(this IMutableStableCollection<T> stableCollection, params T[] elements)
	{
		bool result = false;
		foreach (T element in elements)
		{
			if (stableCollection.Add(element))
			{
				result = true;
			}
		}
		return result;
	}

	private static IStableCollection<T> ConvertToListIfNotCollection<T>(this IEnumerable<T> enumerable)
	{
		return (enumerable as IStableCollection<T>) ?? enumerable.ToPersistentStableList();
	}

	public static bool RemoveAll<T>(this IMutableStableCollection<T> stableCollection, IEnumerable<T> elements)
	{
		IStableCollection<T> stableCollection2 = elements.ConvertToListIfNotCollection();
		bool result = false;
		foreach (T item in stableCollection2)
		{
			if (stableCollection.Remove(item))
			{
				result = true;
			}
		}
		return result;
	}

	public static bool RemoveAll<T>(this IMutableStableCollection<T> stableCollection, ReadOnlySpan<T> elements)
	{
		bool result = false;
		ReadOnlySpan<T> readOnlySpan = elements;
		for (int i = 0; i < readOnlySpan.Length; i++)
		{
			T element = readOnlySpan[i];
			if (stableCollection.Remove(element))
			{
				result = true;
			}
		}
		return result;
	}

	public static bool RemoveAll<T>(this IMutableStableCollection<T> stableCollection, params T[] elements)
	{
		bool result = false;
		foreach (T element in elements)
		{
			if (stableCollection.Remove(element))
			{
				result = true;
			}
		}
		return result;
	}

	public static bool RetainAll<T>(this IMutableStableCollection<T> stableCollection, IEnumerable<T> elements)
	{
		return stableCollection.RetainAll(elements.ConvertToListIfNotCollection());
	}

	public static bool RetainAll<T>(this IMutableStableCollection<T> stableCollection, ReadOnlySpan<T> elements)
	{
		return stableCollection.RetainAll(elements.ToArray());
	}

	public static bool RetainAll<T>(this IMutableStableCollection<T> stableCollection, params T[] elements)
	{
		return stableCollection.RetainAll(elements.ConvertToListIfNotCollection());
	}
}

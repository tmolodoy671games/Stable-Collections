using System;
using System.Collections.Generic;

namespace StableCollections;

public static class StableCollectionExtensions
{
	public static IStableCollection<T> AsStableCollection<T>(this IStableCollection<T> stableCollection)
	{
		return stableCollection;
	}

	public static bool IsNotEmpty<T>(this IStableCollection<T> stableCollection)
	{
		return !stableCollection.IsEmpty();
	}

	public static bool IsNullOrEmpty<T>(this IStableCollection<T>? stableCollection)
	{
		return stableCollection?.IsEmpty() ?? true;
	}

	public static IStableCollection<T> OrEmpty<T>(this IStableCollection<T>? stableCollection)
	{
		return stableCollection ?? IPersistentStableList.Empty<T>();
	}

	public static bool ContainsAll<T>(this IStableCollection<T> stableCollection, IEnumerable<T> elements)
	{
		foreach (T element in elements)
		{
			if (!stableCollection.Contains(element))
			{
				return false;
			}
		}
		return true;
	}

	public static bool ContainsAll<T>(this IStableCollection<T> stableCollection, params T[] elements)
	{
		foreach (T element in elements)
		{
			if (!stableCollection.Contains(element))
			{
				return false;
			}
		}
		return true;
	}

	public static bool ContainsAll<T>(this IStableCollection<T> stableCollection, ReadOnlySpan<T> elements)
	{
		ReadOnlySpan<T> readOnlySpan = elements;
		for (int i = 0; i < readOnlySpan.Length; i++)
		{
			T element = readOnlySpan[i];
			if (!stableCollection.Contains(element))
			{
				return false;
			}
		}
		return true;
	}
}

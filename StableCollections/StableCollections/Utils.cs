using System.Collections.Generic;
using System.Text;

namespace StableCollections;

internal static class Utils
{
	public static string FormatAsCollection<T>(this IEnumerable<T> collection)
	{
		StringBuilder stringBuilder = new StringBuilder("[");
		stringBuilder.Append(string.Join(", ", collection));
		stringBuilder.Append("]");
		return stringBuilder.ToString();
	}

	public static int ListHashCode<T>(this IEnumerable<T> collection)
	{
		int num = 1;
		foreach (T item in collection)
		{
			num = 31 * num + ((!item.IsNull()) ? item.GetHashCode() : 0);
		}
		return num;
	}

	public static bool ListEquals<T>(this IStableList<T> list, object? obj)
	{
		if (list == obj)
		{
			return true;
		}
		if (!(obj is IStableList<T> stableList))
		{
			return false;
		}
		if (list.Size != stableList.Size)
		{
			return false;
		}
		EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
		for (int i = 0; i < list.Size; i++)
		{
			if (!equalityComparer.Equals(list[i], stableList[i]))
			{
				return false;
			}
		}
		return true;
	}

	public static bool SetEquals<T>(this HashSet<T> set, object self, object? obj)
	{
		if (self == obj)
		{
			return true;
		}
		if (!(obj is IStableSet<T> stableSet))
		{
			return false;
		}
		if (set.Count != stableSet.Size)
		{
			return false;
		}
		foreach (T item in set)
		{
			if (!stableSet.Contains(item))
			{
				return false;
			}
		}
		return true;
	}

	public static int SetHashCode<T>(this IEnumerable<T> set)
	{
		int num = 0;
		foreach (T item in set)
		{
			num += ((!item.IsNull()) ? item.GetHashCode() : 0);
		}
		return num;
	}

	public static bool MapEquals<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, object self, object? obj)
	{
		if (self == obj)
		{
			return true;
		}
		if (!(obj is IStableMap<TKey, TValue> stableMap) || stableMap.Size != dictionary.Count)
		{
			return false;
		}
		EqualityComparer<TValue> equalityComparer = EqualityComparer<TValue>.Default;
		foreach (KeyValuePair<TKey, TValue> item in dictionary)
		{
			if (!stableMap.ContainsKey(item.Key) || !equalityComparer.Equals(item.Value, stableMap[item.Key]))
			{
				return false;
			}
		}
		return true;
	}

	public static int MapHashCode<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
	{
		int num = 0;
		foreach (KeyValuePair<TKey, TValue> item in dictionary)
		{
			int num2 = ((!item.Key.IsNull()) ? item.Key.GetHashCode() : 0);
			int num3 = ((!item.Value.IsNull()) ? item.Value.GetHashCode() : 0);
			num += num2 ^ num3;
		}
		return num;
	}

	public static bool IsNull<T>(this T value)
	{
		if (default(T) == null)
		{
			return value == null;
		}
		return false;
	}

	public static string ToString<T>(this T value)
	{
		if (!value.IsNull())
		{
			return value.ToString();
		}
		return "Null";
	}

	public static int GetHashCode<T>(this T value)
	{
		if (!value.IsNull())
		{
			return value.GetHashCode();
		}
		return 0;
	}
}

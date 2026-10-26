using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public static class StableMapExtensions
{
	public static bool IsNotEmpty<TKey, TValue>(this IStableMap<TKey, TValue> map)
	{
		return !map.IsEmpty();
	}

	public static bool IsNullOrEmpty<TKey, TValue>(this IStableMap<TKey, TValue>? map)
	{
		return map?.IsEmpty() ?? true;
	}

	public static IStableMap<TKey, TValue> OrEmpty<TKey, TValue>(this IStableMap<TKey, TValue>? map)
	{
		return map ?? IPersistentStableMap.Empty<TKey, TValue>();
	}

	public static IStableMap<TKey, TValue> IfEmpty<TKey, TValue>(this IStableMap<TKey, TValue> map, Func<IStableMap<TKey, TValue>> defaultValue)
	{
		if (!map.IsEmpty())
		{
			return map;
		}
		return defaultValue();
	}

	public static TValue GetValue<TKey, TValue>(this IStableMap<TKey, TValue> map, TKey key)
	{
		if (!map.ContainsKey(key))
		{
			throw new KeyNotFoundException("Key " + Utils.ToString(key) + " is missing in the map.");
		}
		return map[key];
	}

	public static TValue GetOrElse<TKey, TValue>(this IStableMap<TKey, TValue> map, TKey key, Func<TValue> defaultValue)
	{
		if (!map.ContainsKey(key))
		{
			return defaultValue();
		}
		return map[key];
	}

	public static TValue GetOrPut<TKey, TValue>(this IMutableStableMap<TKey, TValue> map, TKey key, Func<TValue> defaultValue)
	{
		if (map.ContainsKey(key))
		{
			return map[key];
		}
		return map[key] = defaultValue();
	}

	public static void Put<TKey, TValue>(this IMutableStableMap<TKey, TValue> map, TKey key, TValue value)
	{
		map[key] = value;
	}

	public static void PutAll<TKey, TValue>(this IMutableStableMap<TKey, TValue> map, IEnumerable<(TKey Key, TValue Value)> pairs)
	{
		foreach (var pair in pairs)
		{
			TKey item = pair.Key;
			TValue item2 = pair.Value;
			map[item] = item2;
		}
	}

	public static bool RemoveAll<TKey, TValue>(this IMutableStableMap<TKey, TValue> map, Func<StableMapEntry<TKey, TValue>, bool> predicate)
	{
		IStableSet<TKey> keys = map.Filter(predicate).Keys;
		foreach (TKey item in keys)
		{
			map.Remove(item);
		}
		return keys.IsNotEmpty();
	}

	public static IPersistentStableMap<TKey, TValue> Filter<TKey, TValue>(this IStableMap<TKey, TValue> map, Func<StableMapEntry<TKey, TValue>, bool> predicate)
	{
		return map.Where(predicate).ToPersistentStableMap();
	}

	public static IPersistentStableMap<TKey, TValue> FilterNot<TKey, TValue>(this IStableMap<TKey, TValue> map, Func<StableMapEntry<TKey, TValue>, bool> predicate)
	{
		return map.Filter((StableMapEntry<TKey, TValue> it) => !predicate(it));
	}

	public static IPersistentStableMap<TKey, TValue> FilterKeys<TKey, TValue>(this IStableMap<TKey, TValue> map, Func<TKey, bool> predicate)
	{
		return map.Filter((StableMapEntry<TKey, TValue> it) => predicate(it.Key));
	}

	public static IPersistentStableMap<TKey, TValue> FilterValues<TKey, TValue>(this IStableMap<TKey, TValue> map, Func<TValue, bool> predicate)
	{
		return map.Filter((StableMapEntry<TKey, TValue> it) => predicate(it.Value));
	}

	public static IPersistentStableMap<TKey, R> MapValues<TKey, TValue, R>(this IStableMap<TKey, TValue> map, Func<StableMapEntry<TKey, TValue>, R> transform)
	{
		return map.Select((StableMapEntry<TKey, TValue> it) => (Key: it.Key, transform(it))).ToPersistentStableMap();
	}

	public static IPersistentStableMap<R, TValue> MapKeys<TKey, TValue, R>(this IStableMap<TKey, TValue> map, Func<StableMapEntry<TKey, TValue>, R> transform)
	{
		return map.Select((StableMapEntry<TKey, TValue> it) => (transform(it), Value: it.Value)).ToPersistentStableMap();
	}

	public static void ForEach<TKey, TValue>(this IStableMap<TKey, TValue> map, Action<TKey, TValue> action)
	{
		foreach (StableMapEntry<TKey, TValue> item in map)
		{
			action(item.Key, item.Value);
		}
	}

	public static IPersistentStableList<(TKey Key, TValue Value)> ToPairs<TKey, TValue>(this IStableMap<TKey, TValue> map)
	{
		return map.Map((StableMapEntry<TKey, TValue> it) => (Key: it.Key, Value: it.Value));
	}

	public static IPersistentStableMap<TKey, TValue> ToPersistentStableMap<TKey, TValue>(this IStableMap<TKey, TValue> map)
	{
		return (map as IPersistentStableMap<TKey, TValue>) ?? StableEnumerableExtensions.ToPersistentStableMap(map);
	}

	public static IPersistentStableMap<TKey, TValue> Plus<TKey, TValue>(this IStableMap<TKey, TValue> map, (TKey Key, TValue Value) pair)
	{
		return map.ToPersistentStableMap().Put(pair.Key, pair.Value);
	}

	public static IPersistentStableMap<TKey, TValue> Plus<TKey, TValue>(this IStableMap<TKey, TValue> map, IEnumerable<(TKey Key, TValue Value)> pairs)
	{
		IMutableStableMap<TKey, TValue> mutableStableMap = (IMutableStableMap<TKey, TValue>)map.ToPersistentStableMap().Builder();
		mutableStableMap.PutAll(pairs);
		return ((IPersistentStableMap<TKey, TValue>.IBuilder)mutableStableMap).Build();
	}

	public static IPersistentStableMap<TKey, TValue> Plus<TKey, TValue>(this IStableMap<TKey, TValue> map, IStableMap<TKey, TValue> other)
	{
		return map.ToPersistentStableMap().PutAll(other);
	}

	public static IPersistentStableMap<TKey, TValue> Minus<TKey, TValue>(this IStableMap<TKey, TValue> map, TKey key)
	{
		return map.ToPersistentStableMap().Remove(key);
	}

	public static IPersistentStableMap<TKey, TValue> Minus<TKey, TValue>(this IStableMap<TKey, TValue> map, IEnumerable<TKey> keys)
	{
		HashSet<TKey> excluded = keys.ToHashSet();
		return map.FilterKeys((TKey it) => !excluded.Contains(it));
	}
}

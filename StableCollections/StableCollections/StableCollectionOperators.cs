using System.Collections.Generic;

namespace StableCollections;

public static class StableCollectionOperators
{
	extension<T>(IStableList<T>)
	{
		public static IPersistentStableList<T> operator +(IStableList<T> list, T element)
		{
			return list.Plus(element);
		}

		public static IPersistentStableList<T> operator +(IStableList<T> list, IEnumerable<T> elements)
		{
			return list.Plus(elements);
		}

		public static IPersistentStableList<T> operator -(IStableList<T> list, T element)
		{
			return list.Minus(element);
		}

		public static IPersistentStableList<T> operator -(IStableList<T> list, IEnumerable<T> elements)
		{
			return list.Minus(elements);
		}
	}

	extension<T>(IStableSet<T>)
	{
		public static IPersistentStableSet<T> operator +(IStableSet<T> set, T element)
		{
			return set.Plus(element);
		}

		public static IPersistentStableSet<T> operator +(IStableSet<T> set, IEnumerable<T> elements)
		{
			return set.Plus(elements);
		}

		public static IPersistentStableSet<T> operator -(IStableSet<T> set, T element)
		{
			return set.Minus(element);
		}

		public static IPersistentStableSet<T> operator -(IStableSet<T> set, IEnumerable<T> elements)
		{
			return set.Minus(elements);
		}
	}

	extension<TKey, TValue>(IStableMap<TKey, TValue>)
	{
		public static IPersistentStableMap<TKey, TValue> operator +(IStableMap<TKey, TValue> map, (TKey Key, TValue Value) pair)
		{
			return map.Plus(pair);
		}

		public static IPersistentStableMap<TKey, TValue> operator +(IStableMap<TKey, TValue> map, IStableMap<TKey, TValue> other)
		{
			return map.Plus(other);
		}

		public static IPersistentStableMap<TKey, TValue> operator -(IStableMap<TKey, TValue> map, TKey key)
		{
			return map.Minus(key);
		}
	}
}

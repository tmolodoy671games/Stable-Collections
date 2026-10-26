using System;
using System.Linq;
using StableCollections.Array2D.Mutable;
using StableCollections.Array2D.Persistent;
using StableCollections.Map.Persistent;
using StableCollections.Set.Mutable;
using StableCollections.Set.Persistent;

namespace StableCollections;

public static class StableCollectionsFactory
{
	public static IMutableStableArray2D<T> MutableStableArray2DOf<T>(ReadOnlySpan<T[]> elements)
	{
		return new MutableStableArray2DImpl<T>(elements);
	}

	public static IMutableStableArray2D<T> MutableStableArray2DOf<T>(params T[][] elements)
	{
		return new MutableStableArray2DImpl<T>(elements);
	}

	public static IPersistentStableArray2D<T> PersistentStableArray2DOf<T>(ReadOnlySpan<T[]> elements)
	{
		return new PersistentStableArray2DImpl<T>(elements);
	}

	public static IPersistentStableArray2D<T> PersistentStableArray2DOf<T>(params T[][] elements)
	{
		return new PersistentStableArray2DImpl<T>(elements);
	}

	public static IPersistentStableList<T> BuildStableList<T>(Action<IMutableStableList<T>> builderAction)
	{
		IMutableStableList<T> mutableStableList = MutableStableListOf<T>();
		builderAction(mutableStableList);
		return ((IPersistentStableList<T>.IBuilder)mutableStableList).Build();
	}

	public static IPersistentStableSet<T> BuildStableSet<T>(Action<IMutableStableSet<T>> builderAction)
	{
		IMutableStableSet<T> mutableStableSet = MutableStableSetOf<T>();
		builderAction(mutableStableSet);
		return ((IPersistentStableSet<T>.IBuilder)mutableStableSet).Build();
	}

	public static IPersistentStableMap<TKey, TValue> BuildStableMap<TKey, TValue>(Action<IMutableStableMap<TKey, TValue>> builderAction)
	{
		IMutableStableMap<TKey, TValue> mutableStableMap = MutableStableMapOf<TKey, TValue>();
		builderAction(mutableStableMap);
		return ((IPersistentStableMap<TKey, TValue>.IBuilder)mutableStableMap).Build();
	}

	public static IPersistentStableList<T> PersistentStableListOfNotNull<T>(params T?[] elements) where T : class
	{
		return elements.FilterNotNull();
	}

	public static IPersistentStableList<T> PersistentStableListOfNotNull<T>(params T?[] elements) where T : struct
	{
		return elements.FilterNotNull();
	}

	public static IMutableStableList<T> MutableStableListOf<T>(ReadOnlySpan<T> elements)
	{
		return new MutableStableListImpl<T>(elements);
	}

	public static IMutableStableList<T> MutableStableListOf<T>(params T[] elements)
	{
		return new MutableStableListImpl<T>(elements);
	}

	public static IMutableStableList<T> MutableStableListOf<T>(T element)
	{
		return new MutableStableListImpl<T>(new ReadOnlySpan<T>(new T[1] { element }));
	}

	public static IMutableStableList<T> MutableStableListOf<T>()
	{
		return new MutableStableListImpl<T>();
	}

	public static IMutableStableList<T> MutableStableList<T>(int count, Func<int, T> init)
	{
		return new MutableStableListImpl<T>(count, init);
	}

	public static IPersistentStableList<T> PersistentStableListOf<T>(ReadOnlySpan<T> elements)
	{
		if (elements.IsEmpty)
		{
			return IPersistentStableList.Empty<T>();
		}
		return new PersistentStableListImpl<T>(elements);
	}

	public static IPersistentStableList<T> PersistentStableListOf<T>(params T[] elements)
	{
		if (elements.Length == 0)
		{
			return IPersistentStableList.Empty<T>();
		}
		return new PersistentStableListImpl<T>(elements);
	}

	public static IPersistentStableList<T> PersistentStableListOf<T>(T element)
	{
		return new PersistentStableListImpl<T>(new ReadOnlySpan<T>(new T[1] { element }));
	}

	public static IPersistentStableList<T> PersistentStableListOf<T>()
	{
		return IPersistentStableList.Empty<T>();
	}

	public static IPersistentStableList<T> PersistentStableList<T>(int count, Func<int, T> init)
	{
		return new PersistentStableListImpl<T>(count, init);
	}

	public static IMutableStableMap<TKey, TValue> MutableStableMapOf<TKey, TValue>(ReadOnlySpan<(TKey Key, TValue Value)> entries)
	{
		return new MutableStableMapImpl<TKey, TValue>(entries);
	}

	public static IMutableStableMap<TKey, TValue> MutableStableMapOf<TKey, TValue>(params (TKey Key, TValue Value)[] entries)
	{
		return new MutableStableMapImpl<TKey, TValue>(entries);
	}

	public static IMutableStableMap<TKey, TValue> MutableStableMapOf<TKey, TValue>((TKey Key, TValue Value) entry)
	{
		return new MutableStableMapImpl<TKey, TValue>(new ReadOnlySpan<(TKey, TValue)>(new (TKey, TValue)[1] { entry }));
	}

	public static IMutableStableMap<TKey, TValue> MutableStableMapOf<TKey, TValue>()
	{
		return new MutableStableMapImpl<TKey, TValue>(Enumerable.Empty<(TKey, TValue)>());
	}

	public static IPersistentStableMap<TKey, TValue> PersistentStableMapOf<TKey, TValue>(ReadOnlySpan<(TKey Key, TValue Value)> entries)
	{
		return new PersistentStableMapImpl<TKey, TValue>(entries);
	}

	public static IPersistentStableMap<TKey, TValue> PersistentStableMapOf<TKey, TValue>(params (TKey Key, TValue Value)[] entries)
	{
		return new PersistentStableMapImpl<TKey, TValue>(entries);
	}

	public static IPersistentStableMap<TKey, TValue> PersistentStableMapOf<TKey, TValue>((TKey Key, TValue Value) entry)
	{
		return new PersistentStableMapImpl<TKey, TValue>(new ReadOnlySpan<(TKey, TValue)>(new (TKey, TValue)[1] { entry }));
	}

	public static IPersistentStableMap<TKey, TValue> PersistentStableMapOf<TKey, TValue>()
	{
		return new PersistentStableMapImpl<TKey, TValue>(Enumerable.Empty<(TKey, TValue)>());
	}

	public static IMutableStableSet<T> MutableStableSetOf<T>(ReadOnlySpan<T> elements)
	{
		return new MutableStableSetImpl<T>(elements);
	}

	public static IMutableStableSet<T> MutableStableSetOf<T>(params T[] elements)
	{
		return new MutableStableSetImpl<T>(elements);
	}

	public static IMutableStableSet<T> MutableStableSetOf<T>(T element)
	{
		return new MutableStableSetImpl<T>(new ReadOnlySpan<T>(new T[1] { element }));
	}

	public static IMutableStableSet<T> MutableStableSetOf<T>()
	{
		return new MutableStableSetImpl<T>(default(ReadOnlySpan<T>));
	}

	public static IPersistentStableSet<T> PersistentStableSetOf<T>(ReadOnlySpan<T> elements)
	{
		return new PersistentStableSetImpl<T>(elements);
	}

	public static IPersistentStableSet<T> PersistentStableSetOf<T>(params T[] elements)
	{
		return new PersistentStableSetImpl<T>(elements);
	}

	public static IPersistentStableSet<T> PersistentStableSetOf<T>(T element)
	{
		return new PersistentStableSetImpl<T>(new ReadOnlySpan<T>(new T[1] { element }));
	}

	public static IPersistentStableSet<T> PersistentStableSetOf<T>()
	{
		return new PersistentStableSetImpl<T>(default(ReadOnlySpan<T>));
	}
}

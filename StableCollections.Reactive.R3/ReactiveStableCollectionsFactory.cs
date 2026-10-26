using System;
using System.Collections.Generic;
using SharpExtensions;

namespace StableCollections.Reactive.R3;

[AutoGlobalUsingStatic]
public static class ReactiveStableCollectionsFactory
{
	public static IMutableReactiveStableArray2D<T> MutableReactiveStableArray2DOf<T>(ReadOnlySpan<T[]> elements)
	{
		return new MutableReactiveStableArray2DImpl<T>(elements);
	}

	public static IMutableReactiveStableArray2D<T> MutableReactiveStableArray2DOf<T>(params T[][] elements)
	{
		return new MutableReactiveStableArray2DImpl<T>(elements);
	}

	public static IMutableReactiveStableArray2D<T> MutableReactiveStableArray2DOf<T>(IEnumerable<T[]> elements)
	{
		return new MutableReactiveStableArray2DImpl<T>(elements);
	}

	public static IMutableReactiveStableList<T> MutableReactiveStableListOf<T>(ReadOnlySpan<T> elements)
	{
		return new MutableReactiveStableListImpl<T>(elements);
	}

	public static IMutableReactiveStableList<T> MutableReactiveStableListOf<T>(params T[] elements)
	{
		return new MutableReactiveStableListImpl<T>(elements);
	}

	public static IMutableReactiveStableList<T> MutableReactiveStableListOf<T>(T element)
	{
		return new MutableReactiveStableListImpl<T>(new T[1] { element });
	}

	public static IMutableReactiveStableList<T> MutableReactiveStableListOf<T>()
	{
		return new MutableReactiveStableListImpl<T>(Array.Empty<T>());
	}

	public static IMutableReactiveStableMap<TKey, TValue> MutableReactiveStableMapOf<TKey, TValue>(ReadOnlySpan<(TKey Key, TValue Value)> entries)
	{
		return new MutableReactiveStableMapImpl<TKey, TValue>(entries);
	}

	public static IMutableReactiveStableMap<TKey, TValue> MutableReactiveStableMapOf<TKey, TValue>(params (TKey Key, TValue Value)[] entries)
	{
		return new MutableReactiveStableMapImpl<TKey, TValue>(entries);
	}

	public static IMutableReactiveStableMap<TKey, TValue> MutableReactiveStableMapOf<TKey, TValue>((TKey Key, TValue Value) entry)
	{
		return new MutableReactiveStableMapImpl<TKey, TValue>(new (TKey, TValue)[1] { entry });
	}

	public static IMutableReactiveStableMap<TKey, TValue> MutableReactiveStableMapOf<TKey, TValue>()
	{
		return new MutableReactiveStableMapImpl<TKey, TValue>(Array.Empty<(TKey, TValue)>());
	}

	public static IMutableReactiveStableProperty<T> MutableReactiveStablePropertyOf<T>(T initialValue)
	{
		return new MutableReactiveStablePropertyImpl<T>(initialValue);
	}

	public static IMutableReactiveStableSet<T> MutableReactiveStableSetOf<T>(ReadOnlySpan<T> elements)
	{
		return new MutableReactiveStableSetImpl<T>(elements);
	}

	public static IMutableReactiveStableSet<T> MutableReactiveStableSetOf<T>(params T[] elements)
	{
		return new MutableReactiveStableSetImpl<T>(elements);
	}

	public static IMutableReactiveStableSet<T> MutableReactiveStableSetOf<T>(T element)
	{
		return new MutableReactiveStableSetImpl<T>(new T[1] { element });
	}

	public static IMutableReactiveStableSet<T> MutableReactiveStableSetOf<T>()
	{
		return new MutableReactiveStableSetImpl<T>(Array.Empty<T>());
	}
}

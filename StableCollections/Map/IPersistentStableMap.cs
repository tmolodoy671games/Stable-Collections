using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StableCollections.Map.Persistent;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IPersistentStableMap
{
	private static class EmptyHolder<TKey, TValue>
	{
		public static readonly IPersistentStableMap<TKey, TValue> Instance = new PersistentStableMapImpl<TKey, TValue>(Enumerable.Empty<(TKey, TValue)>());
	}

	static IPersistentStableMap<TKey, TValue> Empty<TKey, TValue>()
	{
		return EmptyHolder<TKey, TValue>.Instance;
	}

	static IPersistentStableMap<TKey, TValue> Create<TKey, TValue>(params (TKey Key, TValue Value)[] entries)
	{
		if (entries.Length == 0)
		{
			return Empty<TKey, TValue>();
		}
		return new PersistentStableMapImpl<TKey, TValue>(entries);
	}

	static IPersistentStableMap<TKey, TValue> Create<TKey, TValue>(IEnumerable<(TKey Key, TValue Value)> entries)
	{
		return new PersistentStableMapImpl<TKey, TValue>(entries);
	}

	static IPersistentStableMap<TKey, TValue> Create<TKey, TValue>(ReadOnlySpan<(TKey Key, TValue Value)> entries)
	{
		if (entries.Length == 0)
		{
			return Empty<TKey, TValue>();
		}
		return new PersistentStableMapImpl<TKey, TValue>(entries);
	}
}
public interface IPersistentStableMap<TKey, TValue> : IImmutableStableMap<TKey, TValue>, IStableMap<TKey, TValue>, IEnumerable<StableMapEntry<TKey, TValue>>, IEnumerable
{
	public interface IBuilder
	{
		IPersistentStableMap<TKey, TValue> Build();
	}

	IPersistentStableMap<TKey, TValue> Put(TKey key, TValue value);

	IPersistentStableMap<TKey, TValue> Remove(TKey key);

	IPersistentStableMap<TKey, TValue> PutAll(IStableMap<TKey, TValue> from);

	IPersistentStableMap<TKey, TValue> Clear();

	IBuilder Builder();
}

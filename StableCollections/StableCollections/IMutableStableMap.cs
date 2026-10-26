using System;
using System.Collections;
using System.Collections.Generic;

namespace StableCollections;

public interface IMutableStableMap
{
	static IMutableStableMap<TKey, TValue> Create<TKey, TValue>(params (TKey Key, TValue Value)[] entries)
	{
		return new MutableStableMapImpl<TKey, TValue>(entries);
	}

	static IMutableStableMap<TKey, TValue> Create<TKey, TValue>(IEnumerable<(TKey Key, TValue Value)> entries)
	{
		return new MutableStableMapImpl<TKey, TValue>(entries);
	}

	static IMutableStableMap<TKey, TValue> Create<TKey, TValue>(ReadOnlySpan<(TKey Key, TValue Value)> entries)
	{
		return new MutableStableMapImpl<TKey, TValue>(entries);
	}
}
public interface IMutableStableMap<TKey, TValue> : IStableMap<TKey, TValue>, IEnumerable<StableMapEntry<TKey, TValue>>, IEnumerable
{
	new TValue this[TKey key] { get; set; }

	void Remove(TKey key);

	void PutAll(IStableMap<TKey, TValue> from);

	void Clear();
}

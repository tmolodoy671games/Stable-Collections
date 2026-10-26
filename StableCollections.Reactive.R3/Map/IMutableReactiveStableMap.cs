using System;
using System.Collections;
using System.Collections.Generic;

namespace StableCollections.Reactive.R3;

public interface IMutableReactiveStableMap
{
	static IMutableReactiveStableMap<TKey, TValue> Create<TKey, TValue>()
	{
		return new MutableReactiveStableMapImpl<TKey, TValue>(Array.Empty<(TKey, TValue)>());
	}

	static IMutableReactiveStableMap<TKey, TValue> Create<TKey, TValue>(params (TKey Key, TValue Value)[] entries)
	{
		return new MutableReactiveStableMapImpl<TKey, TValue>(entries);
	}

	static IMutableReactiveStableMap<TKey, TValue> Create<TKey, TValue>(IEnumerable<(TKey Key, TValue Value)> entries)
	{
		return new MutableReactiveStableMapImpl<TKey, TValue>(entries);
	}

	static IMutableReactiveStableMap<TKey, TValue> Create<TKey, TValue>(ReadOnlySpan<(TKey Key, TValue Value)> entries)
	{
		return new MutableReactiveStableMapImpl<TKey, TValue>(entries);
	}
}
public interface IMutableReactiveStableMap<TKey, TValue> : IMutableStableMap<TKey, TValue>, IStableMap<TKey, TValue>, IEnumerable<StableMapEntry<TKey, TValue>>, IEnumerable, IReactiveStableMap<TKey, TValue>, IObservableSource<IPersistentStableMap<TKey, TValue>>, IDisposable
{
}

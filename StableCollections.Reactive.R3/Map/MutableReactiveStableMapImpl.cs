using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using R3;

namespace StableCollections.Reactive.R3;

internal sealed class MutableReactiveStableMapImpl<TKey, TValue> : IMutableReactiveStableMap<TKey, TValue>, IMutableStableMap<TKey, TValue>, IStableMap<TKey, TValue>, IEnumerable<StableMapEntry<TKey, TValue>>, IEnumerable, IReactiveStableMap<TKey, TValue>, IObservableSource<IPersistentStableMap<TKey, TValue>>, IDisposable
{
	private readonly IMutableReactiveStableProperty<IPersistentStableMap<TKey, TValue>> _map;

	public int Size => ((IStableMap<TKey, TValue>)(object)_map.Value).Size;

	public IStableSet<TKey> Keys => ((IStableMap<TKey, TValue>)(object)_map.Value).Keys;

	public IStableCollection<TValue> Values => ((IStableMap<TKey, TValue>)(object)_map.Value).Values;

	public IStableSet<StableMapEntry<TKey, TValue>> Entries => ((IStableMap<TKey, TValue>)(object)_map.Value).Entries;

	public TValue this[TKey key]
	{
		get
		{
			return ((IStableMap<TKey, TValue>)(object)_map.Value)[key];
		}
		set
		{
			IPersistentStableMap<TKey, TValue> value2 = _map.Value;
			if (!((IStableMap<TKey, TValue>)(object)value2).ContainsKey(key) || !EqualityComparer<TValue>.Default.Equals(((IStableMap<TKey, TValue>)(object)value2)[key], value))
			{
				_map.Value = value2.Put(key, value);
			}
		}
	}

	public MutableReactiveStableMapImpl(IEnumerable<(TKey Key, TValue Value)> entries)
	{
		_map = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableMap<TKey, TValue>>(StableCollectionsFactory.PersistentStableMapOf<TKey, TValue>(entries.ToArray()));
	}

	public MutableReactiveStableMapImpl((TKey Key, TValue Value)[] entries)
	{
		_map = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableMap<TKey, TValue>>(StableCollectionsFactory.PersistentStableMapOf<TKey, TValue>(entries));
	}

	public MutableReactiveStableMapImpl(ReadOnlySpan<(TKey Key, TValue Value)> entries)
	{
		_map = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableMap<TKey, TValue>>(StableCollectionsFactory.PersistentStableMapOf<TKey, TValue>(entries));
	}

	public IStableMap<TKey, TValue>.Enumerator GetEnumerator()
	{
		return ((IStableMap<TKey, TValue>)(object)_map.Value).GetEnumerator();
	}

	public bool IsEmpty()
	{
		return ((IStableMap<TKey, TValue>)(object)_map.Value).IsEmpty();
	}

	public bool ContainsKey(TKey key)
	{
		return ((IStableMap<TKey, TValue>)(object)_map.Value).ContainsKey(key);
	}

	public bool ContainsValue(TValue value)
	{
		return ((IStableMap<TKey, TValue>)(object)_map.Value).ContainsValue(value);
	}

	public TValue GetOrDefault(TKey key, TValue defaultValue)
	{
		return ((IStableMap<TKey, TValue>)(object)_map.Value).GetOrDefault(key, defaultValue);
	}

	public void Remove(TKey key)
	{
		IPersistentStableMap<TKey, TValue> value = _map.Value;
		if (((IStableMap<TKey, TValue>)(object)value).ContainsKey(key))
		{
			_map.Value = value.Remove(key);
		}
	}

	public void PutAll(IStableMap<TKey, TValue> from)
	{
		_map.Value = _map.Value.PutAll(from);
	}

	public void Clear()
	{
		_map.Value = _map.Value.Clear();
	}

	public Observable<IPersistentStableMap<TKey, TValue>> AsObservable()
	{
		return _map.AsObservable();
	}

	public void Dispose()
	{
		_map.Dispose();
	}

	public override string ToString()
	{
		return ((object)_map.Value).ToString();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StableCollections.Map.Persistent;

internal class PersistentStableMapImpl<TKey, TValue> : IPersistentStableMap<TKey, TValue>, IImmutableStableMap<TKey, TValue>, IStableMap<TKey, TValue>, IEnumerable<StableMapEntry<TKey, TValue>>, IEnumerable
{
	private readonly Dictionary<TKey, TValue> _dictionary;

	public int Size => _dictionary.Count;

	public IStableSet<TKey> Keys => _dictionary.Keys.ToPersistentStableSet();

	public IStableCollection<TValue> Values => _dictionary.Values.ToPersistentStableList();

	public IStableSet<StableMapEntry<TKey, TValue>> Entries => _dictionary.Select((KeyValuePair<TKey, TValue> it) => new StableMapEntry<TKey, TValue>(it.Key, it.Value)).ToPersistentStableSet();

	public TValue this[TKey key] => _dictionary[key];

	public PersistentStableMapImpl(IEnumerable<StableMapEntry<TKey, TValue>> entries)
	{
		_dictionary = new Dictionary<TKey, TValue>();
		foreach (StableMapEntry<TKey, TValue> entry in entries)
		{
			_dictionary[entry.Key] = entry.Value;
		}
	}

	public PersistentStableMapImpl(IEnumerable<(TKey Key, TValue Value)> entries)
	{
		_dictionary = new Dictionary<TKey, TValue>();
		foreach (var entry in entries)
		{
			_dictionary[entry.Key] = entry.Value;
		}
	}

	public PersistentStableMapImpl((TKey Key, TValue Value)[] entries)
	{
		_dictionary = new Dictionary<TKey, TValue>();
		for (int i = 0; i < entries.Length; i++)
		{
			(TKey, TValue) tuple = entries[i];
			_dictionary[tuple.Item1] = tuple.Item2;
		}
	}

	public PersistentStableMapImpl(ReadOnlySpan<(TKey Key, TValue Value)> entries)
	{
		_dictionary = new Dictionary<TKey, TValue>();
		ReadOnlySpan<(TKey, TValue)> readOnlySpan = entries;
		for (int i = 0; i < readOnlySpan.Length; i++)
		{
			(TKey, TValue) tuple = readOnlySpan[i];
			_dictionary[tuple.Item1] = tuple.Item2;
		}
	}

	public PersistentStableMapImpl(Dictionary<TKey, TValue> dictionary)
	{
		_dictionary = dictionary;
	}

	public IStableMap<TKey, TValue>.Enumerator GetEnumerator()
	{
		return new IStableMap<TKey, TValue>.Enumerator(_dictionary);
	}

	public bool IsEmpty()
	{
		return _dictionary.Count == 0;
	}

	public bool ContainsKey(TKey key)
	{
		return _dictionary.ContainsKey(key);
	}

	public bool ContainsValue(TValue value)
	{
		return _dictionary.ContainsValue(value);
	}

	public TValue GetOrDefault(TKey key, TValue defaultValue)
	{
		return _dictionary.GetValueOrDefault(key, defaultValue);
	}

	public IPersistentStableMap<TKey, TValue> Put(TKey key, TValue value)
	{
		Dictionary<TKey, TValue> dictionary = _dictionary.ToDictionary((KeyValuePair<TKey, TValue> it) => it.Key, (KeyValuePair<TKey, TValue> it) => it.Value);
		dictionary[key] = value;
		return new PersistentStableMapImpl<TKey, TValue>(dictionary);
	}

	public IPersistentStableMap<TKey, TValue> Remove(TKey key)
	{
		Dictionary<TKey, TValue> dictionary = _dictionary.ToDictionary((KeyValuePair<TKey, TValue> it) => it.Key, (KeyValuePair<TKey, TValue> it) => it.Value);
		dictionary.Remove(key);
		return new PersistentStableMapImpl<TKey, TValue>(dictionary);
	}

	public IPersistentStableMap<TKey, TValue> PutAll(IStableMap<TKey, TValue> from)
	{
		Dictionary<TKey, TValue> dictionary = _dictionary.ToDictionary((KeyValuePair<TKey, TValue> it) => it.Key, (KeyValuePair<TKey, TValue> it) => it.Value);
		foreach (StableMapEntry<TKey, TValue> entry in from.Entries)
		{
			dictionary[entry.Key] = entry.Value;
		}
		return new PersistentStableMapImpl<TKey, TValue>(dictionary);
	}

	public IPersistentStableMap<TKey, TValue> Clear()
	{
		if (IsEmpty())
		{
			return this;
		}
		return IPersistentStableMap.Empty<TKey, TValue>();
	}

	public IPersistentStableMap<TKey, TValue>.IBuilder Builder()
	{
		return new MutableStableMapImpl<TKey, TValue>(_dictionary.ToDictionary((KeyValuePair<TKey, TValue> it) => it.Key, (KeyValuePair<TKey, TValue> it) => it.Value));
	}

	public override int GetHashCode()
	{
		return _dictionary.MapHashCode();
	}

	public override bool Equals(object? obj)
	{
		return _dictionary.MapEquals(this, obj);
	}

	public override string ToString()
	{
		return Entries.FormatAsCollection();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StableCollections.Map.Persistent;

namespace StableCollections;

internal class MutableStableMapImpl<TKey, TValue> : IMutableStableMap<TKey, TValue>, IStableMap<TKey, TValue>, IEnumerable<StableMapEntry<TKey, TValue>>, IEnumerable, IPersistentStableMap<TKey, TValue>.IBuilder
{
	private readonly Dictionary<TKey, TValue> _dictionary;

	public int Size => _dictionary.Count;

	public IStableSet<TKey> Keys => _dictionary.Keys.ToPersistentStableSet();

	public IStableCollection<TValue> Values => _dictionary.Values.ToPersistentStableList();

	public IStableSet<StableMapEntry<TKey, TValue>> Entries => _dictionary.Select((KeyValuePair<TKey, TValue> it) => new StableMapEntry<TKey, TValue>(it.Key, it.Value)).ToPersistentStableSet();

	public TValue this[TKey key]
	{
		get
		{
			return _dictionary[key];
		}
		set
		{
			_dictionary[key] = value;
		}
	}

	public MutableStableMapImpl(IEnumerable<StableMapEntry<TKey, TValue>> entries)
	{
		_dictionary = new Dictionary<TKey, TValue>();
		foreach (StableMapEntry<TKey, TValue> entry in entries)
		{
			_dictionary[entry.Key] = entry.Value;
		}
	}

	public MutableStableMapImpl(IEnumerable<(TKey Key, TValue Value)> entries)
	{
		_dictionary = new Dictionary<TKey, TValue>();
		foreach (var entry in entries)
		{
			_dictionary[entry.Key] = entry.Value;
		}
	}

	public MutableStableMapImpl(ReadOnlySpan<(TKey Key, TValue Value)> entries)
	{
		_dictionary = new Dictionary<TKey, TValue>();
		ReadOnlySpan<(TKey, TValue)> readOnlySpan = entries;
		for (int i = 0; i < readOnlySpan.Length; i++)
		{
			(TKey, TValue) tuple = readOnlySpan[i];
			_dictionary[tuple.Item1] = tuple.Item2;
		}
	}

	public MutableStableMapImpl(Dictionary<TKey, TValue> dictionary)
	{
		_dictionary = dictionary;
	}

	public IStableMap<TKey, TValue>.Enumerator GetEnumerator()
	{
		return new IStableMap<TKey, TValue>.Enumerator(_dictionary);
	}

	public void Remove(TKey key)
	{
		_dictionary.Remove(key);
	}

	public void PutAll(IStableMap<TKey, TValue> from)
	{
		foreach (StableMapEntry<TKey, TValue> entry in from.Entries)
		{
			_dictionary[entry.Key] = entry.Value;
		}
	}

	public void Clear()
	{
		_dictionary.Clear();
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

	public override int GetHashCode()
	{
		return _dictionary.MapHashCode();
	}

	public IPersistentStableMap<TKey, TValue> Build()
	{
		return new PersistentStableMapImpl<TKey, TValue>(_dictionary.ToDictionary((KeyValuePair<TKey, TValue> it) => it.Key, (KeyValuePair<TKey, TValue> it) => it.Value));
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

using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IStableMap<TKey, TValue> : IEnumerable<StableMapEntry<TKey, TValue>>, IEnumerable
{
	public struct Enumerator : IEnumerator<StableMapEntry<TKey, TValue>>, IEnumerator, IDisposable
	{
		private readonly Dictionary<TKey, TValue> _dictionary;

		private Dictionary<TKey, TValue>.Enumerator _dictionaryEnumerator;

		public StableMapEntry<TKey, TValue> Current => new StableMapEntry<TKey, TValue>(_dictionaryEnumerator.Current.Key, _dictionaryEnumerator.Current.Value);

		object IEnumerator.Current => Current;

		internal Enumerator(Dictionary<TKey, TValue> dictionary)
		{
			_dictionary = dictionary;
			_dictionaryEnumerator = dictionary.GetEnumerator();
		}

		public bool MoveNext()
		{
			return _dictionaryEnumerator.MoveNext();
		}

		public void Reset()
		{
			_dictionaryEnumerator.Dispose();
			_dictionaryEnumerator = _dictionary.GetEnumerator();
		}

		public void Dispose()
		{
			_dictionaryEnumerator.Dispose();
		}
	}

	int Size { get; }

	IStableSet<TKey> Keys { get; }

	IStableCollection<TValue> Values { get; }

	IStableSet<StableMapEntry<TKey, TValue>> Entries { get; }

	TValue this[TKey key] { get; }

	new Enumerator GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	IEnumerator<StableMapEntry<TKey, TValue>> IEnumerable<StableMapEntry<TKey, TValue>>.GetEnumerator()
	{
		return GetEnumerator();
	}

	bool IsEmpty();

	bool ContainsKey(TKey key);

	bool ContainsValue(TValue value);

	TValue GetOrDefault(TKey key, TValue defaultValue);
}

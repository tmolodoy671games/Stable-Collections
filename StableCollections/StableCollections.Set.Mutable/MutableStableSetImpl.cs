using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StableCollections.Set.Persistent;

namespace StableCollections.Set.Mutable;

internal class MutableStableSetImpl<T> : IMutableStableSet<T>, IMutableStableCollection<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IStableSet<T>, IPersistentStableSet<T>.IBuilder
{
	private readonly HashSet<T> _set;

	public int Size => _set.Count;

	public MutableStableSetImpl(IEnumerable<T> elements)
	{
		_set = elements.ToHashSet();
	}

	public MutableStableSetImpl(T[] elements)
	{
		_set = elements.ToHashSet();
	}

	public MutableStableSetImpl(ReadOnlySpan<T> elements)
	{
		HashSet<T> hashSet = new HashSet<T>();
		ReadOnlySpan<T>.Enumerator enumerator = elements.GetEnumerator();
		while (enumerator.MoveNext())
		{
			T current = enumerator.Current;
			hashSet.Add(current);
		}
		_set = hashSet;
	}

	public bool IsEmpty()
	{
		return _set.Count == 0;
	}

	public bool Contains(T element)
	{
		return _set.Contains(element);
	}

	public IEnumerator<T> GetEnumerator()
	{
		return _set.GetEnumerator();
	}

	IStableSet<T>.Enumerator IStableSet<T>.GetEnumerator()
	{
		return new IStableSet<T>.Enumerator(_set);
	}

	public bool Add(T element)
	{
		return _set.Add(element);
	}

	public bool Remove(T element)
	{
		return _set.Remove(element);
	}

	public bool AddAll(IStableCollection<T> elements)
	{
		int count = _set.Count;
		foreach (T element in elements)
		{
			_set.Add(element);
		}
		return count < _set.Count;
	}

	public bool RemoveAll(IStableCollection<T> elements)
	{
		if (elements == this)
		{
			bool result = _set.Count > 0;
			_set.Clear();
			return result;
		}
		int count = _set.Count;
		foreach (T element in elements)
		{
			_set.Remove(element);
		}
		return count > _set.Count;
	}

	public bool RetainAll(IStableCollection<T> elements)
	{
		bool result = false;
		foreach (T item in _set.ToPersistentStableList())
		{
			if (!elements.Contains(item))
			{
				_set.Remove(item);
				result = true;
			}
		}
		return result;
	}

	public void Clear()
	{
		_set.Clear();
	}

	public override string ToString()
	{
		return _set.FormatAsCollection();
	}

	public IPersistentStableSet<T> Build()
	{
		return new PersistentStableSetImpl<T>(_set.ToHashSet());
	}

	public override bool Equals(object? obj)
	{
		return _set.SetEquals(this, obj);
	}

	public override int GetHashCode()
	{
		return _set.SetHashCode();
	}
}

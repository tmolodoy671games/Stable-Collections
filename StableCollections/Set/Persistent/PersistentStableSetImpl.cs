using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StableCollections.Set.Mutable;

namespace StableCollections.Set.Persistent;

internal class PersistentStableSetImpl<T> : IPersistentStableSet<T>, IImmutableStableSet<T>, IStableSet<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable
{
	private readonly HashSet<T> _set;

	public int Size => _set.Count;

	public PersistentStableSetImpl(IEnumerable<T> elements)
	{
		_set = elements.ToHashSet();
	}

	public PersistentStableSetImpl(T[] elements)
	{
		_set = elements.ToHashSet();
	}

	public PersistentStableSetImpl(ReadOnlySpan<T> elements)
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

	public IPersistentStableSet<T> Add(T element)
	{
		HashSet<T> hashSet = _set.ToHashSet();
		hashSet.Add(element);
		return new PersistentStableSetImpl<T>(hashSet);
	}

	public IPersistentStableSet<T> AddAll(IStableCollection<T> elements)
	{
		HashSet<T> hashSet = _set.ToHashSet();
		foreach (T element in elements)
		{
			hashSet.Add(element);
		}
		return new PersistentStableSetImpl<T>(hashSet);
	}

	public IPersistentStableSet<T> Remove(T element)
	{
		HashSet<T> hashSet = _set.ToHashSet();
		hashSet.Remove(element);
		return new PersistentStableSetImpl<T>(hashSet);
	}

	public IPersistentStableSet<T> RemoveAll(IStableCollection<T> elements)
	{
		HashSet<T> hashSet = _set.ToHashSet();
		foreach (T element in elements)
		{
			hashSet.Remove(element);
		}
		return new PersistentStableSetImpl<T>(hashSet);
	}

	public IPersistentStableSet<T> RemoveAll(Func<T, bool> predicate)
	{
		HashSet<T> hashSet = _set.ToHashSet();
		hashSet.RemoveWhere((T it) => predicate(it));
		return new PersistentStableSetImpl<T>(hashSet);
	}

	public IPersistentStableSet<T> RetainAll(IStableCollection<T> elements)
	{
		HashSet<T> hashSet = _set.ToHashSet();
		foreach (T item in _set)
		{
			if (!elements.Contains(item))
			{
				hashSet.Remove(item);
			}
		}
		return new PersistentStableSetImpl<T>(hashSet);
	}

	public IPersistentStableSet<T> Clear()
	{
		if (IsEmpty())
		{
			return this;
		}
		return IPersistentStableSet.Empty<T>();
	}

	public IPersistentStableSet<T>.IBuilder Builder()
	{
		return new MutableStableSetImpl<T>(_set.ToHashSet());
	}

	public override int GetHashCode()
	{
		return _set.SetHashCode();
	}

	public override bool Equals(object? obj)
	{
		return _set.SetEquals(this, obj);
	}

	public override string ToString()
	{
		return _set.FormatAsCollection();
	}
}

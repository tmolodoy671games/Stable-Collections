using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StableCollections;

internal sealed class PersistentStableListImpl<T> : IPersistentStableList<T>, IImmutableStableList<T>, IStableList<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IImmutableStableCollection<T>
{
	private readonly List<T> _list;

	public int Size => _list.Count;

	public T this[int index] => _list[index];

	public PersistentStableListImpl(T[] elements)
	{
		_list = elements.ToList();
	}

	public PersistentStableListImpl(IEnumerable<T> elements)
	{
		_list = elements.ToList();
	}

	public PersistentStableListImpl(ReadOnlySpan<T> elements)
	{
		ReadOnlySpan<T> readOnlySpan = elements;
		List<T> list = new List<T>(readOnlySpan.Length);
		ReadOnlySpan<T>.Enumerator enumerator = readOnlySpan.GetEnumerator();
		while (enumerator.MoveNext())
		{
			T current = enumerator.Current;
			list.Add(current);
		}
		_list = list;
	}

	public PersistentStableListImpl(int count, Func<int, T> init)
	{
		_list = new List<T>(count);
		for (int i = 0; i < count; i++)
		{
			_list.Add(init(i));
		}
	}

	public bool IsEmpty()
	{
		return Size == 0;
	}

	public bool Contains(T element)
	{
		return _list.Contains(element);
	}

	public int IndexOf(T element)
	{
		return _list.IndexOf(element);
	}

	public int LastIndexOf(T element)
	{
		return _list.LastIndexOf(element);
	}

	public IStableList<T> SubList(int fromIndex, int toIndex)
	{
		MutableStableListImpl<T> mutableStableListImpl = new MutableStableListImpl<T>(toIndex - fromIndex);
		for (int i = fromIndex; i < toIndex; i++)
		{
			mutableStableListImpl.Add(_list[i]);
		}
		return mutableStableListImpl;
	}

	IStableList<T>.Enumerator IStableList<T>.GetEnumerator()
	{
		return new IStableList<T>.Enumerator(_list);
	}

	public IEnumerator<T> GetEnumerator()
	{
		return _list.GetEnumerator();
	}

	public override bool Equals(object? obj)
	{
		return this.ListEquals(obj);
	}

	public override int GetHashCode()
	{
		return _list.ListHashCode();
	}

	public override string ToString()
	{
		return _list.FormatAsCollection();
	}

	public IPersistentStableList<T> Add(T element)
	{
		List<T> list = _list.ToList();
		list.Add(element);
		return new PersistentStableListImpl<T>(list);
	}

	public IPersistentStableList<T> AddAll(IStableCollection<T> elements)
	{
		List<T> list = _list.ToList();
		list.AddRange(elements);
		return new PersistentStableListImpl<T>(list);
	}

	public IPersistentStableList<T> Remove(T element)
	{
		List<T> list = _list.ToList();
		list.Remove(element);
		return new PersistentStableListImpl<T>(list);
	}

	public IPersistentStableList<T> RemoveAll(IStableCollection<T> elements)
	{
		List<T> list = _list.ToList();
		foreach (T element in elements)
		{
			list.Remove(element);
		}
		return new PersistentStableListImpl<T>(list);
	}

	public IPersistentStableList<T> RemoveAll(Func<T, bool> predicate)
	{
		List<T> list = _list.ToList();
		list.RemoveAll((T it) => predicate(it));
		return new PersistentStableListImpl<T>(list);
	}

	public IPersistentStableList<T> RetainAll(IStableCollection<T> elements)
	{
		List<T> list = _list.ToList();
		for (int i = 0; i < list.Count; i++)
		{
			if (!elements.Contains(list[i]))
			{
				list.RemoveAt(i);
				i--;
			}
		}
		return new PersistentStableListImpl<T>(list);
	}

	public IPersistentStableList<T> Clear()
	{
		if (IsEmpty())
		{
			return this;
		}
		return IPersistentStableList.Empty<T>();
	}

	public IPersistentStableList<T> AddAll(int index, IStableCollection<T> elements)
	{
		List<T> list = _list.ToList();
		list.InsertRange(index, elements);
		return new PersistentStableListImpl<T>(list);
	}

	public IPersistentStableList<T> Set(int index, T element)
	{
		List<T> list = _list.ToList();
		list[index] = element;
		return new PersistentStableListImpl<T>(list);
	}

	public IPersistentStableList<T> Add(int index, T element)
	{
		List<T> list = _list.ToList();
		list.Insert(index, element);
		return new PersistentStableListImpl<T>(list);
	}

	public IPersistentStableList<T> RemoveAt(int index)
	{
		List<T> list = _list.ToList();
		list.RemoveAt(index);
		return new PersistentStableListImpl<T>(list);
	}

	public IPersistentStableList<T>.IBuilder Builder()
	{
		return new MutableStableListImpl<T>(_list.ToList());
	}
}

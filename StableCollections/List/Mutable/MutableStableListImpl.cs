using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StableCollections;

internal sealed class MutableStableListImpl<T> : IMutableStableList<T>, IStableList<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IMutableStableCollection<T>, IPersistentStableList<T>.IBuilder
{
	private readonly List<T> _list;

	public int Size => _list.Count;

	public T this[int index]
	{
		get
		{
			return _list[index];
		}
		set
		{
			_list[index] = value;
		}
	}

	public MutableStableListImpl(params T[] elements)
	{
		_list = new List<T>(elements);
	}

	public MutableStableListImpl(IEnumerable<T> elements)
	{
		_list = new List<T>(elements);
	}

	public MutableStableListImpl(ReadOnlySpan<T> elements)
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

	public MutableStableListImpl(int capacity)
	{
		_list = new List<T>(capacity);
	}

	public MutableStableListImpl(int count, Func<int, T> init)
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

	public bool Add(T element)
	{
		_list.Add(element);
		return true;
	}

	public bool Remove(T element)
	{
		return _list.Remove(element);
	}

	public bool AddAll(IStableCollection<T> elements)
	{
		int count = _list.Count;
		if (elements == this)
		{
			_list.AddRange(_list.ToArray());
		}
		else
		{
			_list.AddRange(elements);
		}
		return count != _list.Count;
	}

	public bool RemoveAll(IStableCollection<T> elements)
	{
		if (elements == this)
		{
			bool result = _list.Count > 0;
			_list.Clear();
			return result;
		}
		int count = _list.Count;
		foreach (T element in elements)
		{
			_list.Remove(element);
		}
		return count != _list.Count;
	}

	public bool RetainAll(IStableCollection<T> elements)
	{
		bool result = false;
		for (int i = 0; i < _list.Count; i++)
		{
			T element = _list[i];
			if (!elements.Contains(element))
			{
				_list.RemoveAt(i);
				i--;
				result = true;
			}
		}
		return result;
	}

	public void Clear()
	{
		if (_list.Count != 0)
		{
			_list.Clear();
		}
	}

	public bool Contains(T element)
	{
		return _list.Contains(element);
	}

	public int IndexOf(T element)
	{
		return _list.IndexOf(element);
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

	public void Add(int index, T element)
	{
		_list.Insert(index, element);
	}

	public void RemoveAt(int index)
	{
		_list.RemoveAt(index);
	}

	public int LastIndexOf(T element)
	{
		return _list.LastIndexOf(element);
	}

	public IEnumerator<T> GetEnumerator()
	{
		return _list.GetEnumerator();
	}

	public override int GetHashCode()
	{
		return _list.ListHashCode();
	}

	public IPersistentStableList<T> Build()
	{
		return new PersistentStableListImpl<T>(_list.ToList());
	}

	public override bool Equals(object? obj)
	{
		return this.ListEquals(obj);
	}

	public override string ToString()
	{
		return _list.FormatAsCollection();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

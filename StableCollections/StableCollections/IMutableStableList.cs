using System;
using System.Collections;
using System.Collections.Generic;

namespace StableCollections;

public interface IMutableStableList
{
	static IMutableStableList<T> Create<T>()
	{
		return new MutableStableListImpl<T>();
	}

	static IMutableStableList<T> Create<T>(params T[] elements)
	{
		return new MutableStableListImpl<T>(elements);
	}

	static IMutableStableList<T> Create<T>(IEnumerable<T> elements)
	{
		return new MutableStableListImpl<T>(elements);
	}

	static IMutableStableList<T> Create<T>(ReadOnlySpan<T> elements)
	{
		return new MutableStableListImpl<T>(elements);
	}

	static IMutableStableList<T> Create<T>(int count, Func<int, T> init)
	{
		return new MutableStableListImpl<T>(count, init);
	}
}
public interface IMutableStableList<T> : IStableList<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IMutableStableCollection<T>
{
	new T this[int index] { get; set; }

	void Add(int index, T element);

	void RemoveAt(int index);
}

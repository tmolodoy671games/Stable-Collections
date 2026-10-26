using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IPersistentStableList
{
	private static class EmptyHolder<T>
	{
		public static IPersistentStableList<T> Empty { get; } = new PersistentStableListImpl<T>(default(ReadOnlySpan<T>));
	}

	static IPersistentStableList<T> Empty<T>()
	{
		return EmptyHolder<T>.Empty;
	}

	static IPersistentStableList<T> Create<T>()
	{
		return EmptyHolder<T>.Empty;
	}

	static IPersistentStableList<T> Create<T>(params T[] elements)
	{
		if (elements.Length != 0)
		{
			return new PersistentStableListImpl<T>(elements);
		}
		return EmptyHolder<T>.Empty;
	}

	static IPersistentStableList<T> Create<T>(IEnumerable<T> elements)
	{
		return new PersistentStableListImpl<T>(elements);
	}

	static IPersistentStableList<T> Create<T>(ReadOnlySpan<T> elements)
	{
		return new PersistentStableListImpl<T>(elements);
	}

	static IPersistentStableList<T> Create<T>(int count, Func<int, T> init)
	{
		if (count != 0)
		{
			return new PersistentStableListImpl<T>(count, init);
		}
		return EmptyHolder<T>.Empty;
	}
}
public interface IPersistentStableList<T> : IImmutableStableList<T>, IStableList<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IImmutableStableCollection<T>
{
	public interface IBuilder
	{
		IPersistentStableList<T> Build();
	}

	IPersistentStableList<T> Add(T element);

	IPersistentStableList<T> AddAll(IStableCollection<T> elements);

	IPersistentStableList<T> Remove(T element);

	IPersistentStableList<T> RemoveAll(IStableCollection<T> elements);

	IPersistentStableList<T> RemoveAll(Func<T, bool> predicate);

	IPersistentStableList<T> RetainAll(IStableCollection<T> elements);

	IPersistentStableList<T> Clear();

	IPersistentStableList<T> AddAll(int index, IStableCollection<T> elements);

	IPersistentStableList<T> Set(int index, T element);

	IPersistentStableList<T> Add(int index, T element);

	IPersistentStableList<T> RemoveAt(int index);

	IBuilder Builder();
}

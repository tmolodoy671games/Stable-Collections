using System;
using System.Collections;
using System.Collections.Generic;
using StableCollections.Set.Persistent;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IPersistentStableSet
{
	private static class EmptyHolder<T>
	{
		public static IPersistentStableSet<T> Empty { get; } = new PersistentStableSetImpl<T>(default(ReadOnlySpan<T>));
	}

	static IPersistentStableSet<T> Empty<T>()
	{
		return EmptyHolder<T>.Empty;
	}

	static IPersistentStableSet<T> Create<T>()
	{
		return new PersistentStableSetImpl<T>(default(ReadOnlySpan<T>));
	}

	static IPersistentStableSet<T> Create<T>(params T[] elements)
	{
		return new PersistentStableSetImpl<T>(elements);
	}

	static IPersistentStableSet<T> Create<T>(IEnumerable<T> elements)
	{
		return new PersistentStableSetImpl<T>(elements);
	}

	static IPersistentStableSet<T> Create<T>(ReadOnlySpan<T> elements)
	{
		return new PersistentStableSetImpl<T>(elements);
	}
}
public interface IPersistentStableSet<T> : IImmutableStableSet<T>, IStableSet<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable
{
	public interface IBuilder
	{
		IPersistentStableSet<T> Build();
	}

	IPersistentStableSet<T> Add(T element);

	IPersistentStableSet<T> AddAll(IStableCollection<T> elements);

	IPersistentStableSet<T> Remove(T element);

	IPersistentStableSet<T> RemoveAll(IStableCollection<T> elements);

	IPersistentStableSet<T> RemoveAll(Func<T, bool> predicate);

	IPersistentStableSet<T> RetainAll(IStableCollection<T> elements);

	IPersistentStableSet<T> Clear();

	IBuilder Builder();
}

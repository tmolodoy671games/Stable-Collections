using System;
using System.Collections;
using System.Collections.Generic;
using StableCollections.Set.Mutable;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IMutableStableSet
{
	static IMutableStableSet<T> Create<T>()
	{
		return new MutableStableSetImpl<T>(default(ReadOnlySpan<T>));
	}

	static IMutableStableSet<T> Create<T>(params T[] elements)
	{
		return new MutableStableSetImpl<T>(elements);
	}

	static IMutableStableSet<T> Create<T>(IEnumerable<T> elements)
	{
		return new MutableStableSetImpl<T>(elements);
	}

	static IMutableStableSet<T> Create<T>(ReadOnlySpan<T> elements)
	{
		return new MutableStableSetImpl<T>(elements);
	}
}
public interface IMutableStableSet<T> : IMutableStableCollection<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IStableSet<T>
{
}

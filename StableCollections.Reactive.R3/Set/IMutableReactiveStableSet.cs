using System;
using System.Collections;
using System.Collections.Generic;

namespace StableCollections.Reactive.R3;

public interface IMutableReactiveStableSet
{
	static IMutableReactiveStableSet<T> Create<T>()
	{
		return new MutableReactiveStableSetImpl<T>(Array.Empty<T>());
	}

	static IMutableReactiveStableSet<T> Create<T>(params T[] elements)
	{
		return new MutableReactiveStableSetImpl<T>(elements);
	}

	static IMutableReactiveStableSet<T> Create<T>(IEnumerable<T> elements)
	{
		return new MutableReactiveStableSetImpl<T>(elements);
	}

	static IMutableReactiveStableSet<T> Create<T>(ReadOnlySpan<T> elements)
	{
		return new MutableReactiveStableSetImpl<T>(elements);
	}
}
public interface IMutableReactiveStableSet<T> : IMutableStableSet<T>, IMutableStableCollection<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IStableSet<T>, IReactiveStableSet<T>, IObservableSource<IPersistentStableSet<T>>, IDisposable
{
}

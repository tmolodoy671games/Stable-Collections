using System;
using System.Collections;
using System.Collections.Generic;

namespace StableCollections.Reactive.R3;

public interface IMutableReactiveStableList
{
	static IMutableReactiveStableList<T> Create<T>()
	{
		return new MutableReactiveStableListImpl<T>(Array.Empty<T>());
	}

	static IMutableReactiveStableList<T> Create<T>(params T[] elements)
	{
		return new MutableReactiveStableListImpl<T>(elements);
	}

	static IMutableReactiveStableList<T> Create<T>(IEnumerable<T> elements)
	{
		return new MutableReactiveStableListImpl<T>(elements);
	}

	static IMutableReactiveStableList<T> Create<T>(ReadOnlySpan<T> elements)
	{
		return new MutableReactiveStableListImpl<T>(elements);
	}
}
public interface IMutableReactiveStableList<T> : IMutableStableList<T>, IStableList<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IMutableStableCollection<T>, IReactiveStableList<T>, IObservableSource<IPersistentStableList<T>>, IDisposable
{
}

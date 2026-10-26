using System;
using System.Collections.Generic;

namespace StableCollections.Reactive.R3;

public interface IMutableReactiveStableArray2D
{
	static IMutableReactiveStableArray2D<T> Create<T>(ReadOnlySpan<T[]> elements)
	{
		return new MutableReactiveStableArray2DImpl<T>(elements);
	}

	static IMutableReactiveStableArray2D<T> Create<T>(params T[][] elements)
	{
		return new MutableReactiveStableArray2DImpl<T>(elements);
	}

	static IMutableReactiveStableArray2D<T> Create<T>(IEnumerable<T[]> elements)
	{
		return new MutableReactiveStableArray2DImpl<T>(elements);
	}
}
public interface IMutableReactiveStableArray2D<T> : IMutableStableArray2D<T>, IStableArray2D<T>, IReactiveStableArray2D<T>, IObservableSource<IPersistentStableArray2D<T>>, IDisposable
{
}

using System;
using StableCollections.Array2D.Mutable;

namespace StableCollections;

public interface IMutableStableArray2D
{
	static IMutableStableArray2D<T> Create<T>(ReadOnlySpan<T[]> elements)
	{
		return new MutableStableArray2DImpl<T>(elements);
	}

	static IMutableStableArray2D<T> Create<T>(params T[][] elements)
	{
		return new MutableStableArray2DImpl<T>(elements);
	}
}
public interface IMutableStableArray2D<T> : IStableArray2D<T>
{
	new T this[int x, int y] { get; set; }
}

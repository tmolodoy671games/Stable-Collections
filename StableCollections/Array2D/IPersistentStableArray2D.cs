using System;
using StableCollections.Array2D.Persistent;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IPersistentStableArray2D
{
	static IPersistentStableArray2D<T> Create<T>(ReadOnlySpan<T[]> elements)
	{
		return new PersistentStableArray2DImpl<T>(elements);
	}

	static IPersistentStableArray2D<T> Create<T>(params T[][] elements)
	{
		return new PersistentStableArray2DImpl<T>(elements);
	}
}
public interface IPersistentStableArray2D<T> : IImmutableStableArray2D<T>, IStableArray2D<T>
{
	IPersistentStableArray2D<T> Set((int X, int Y) index, T value);
}

using System;
using System.Collections.Generic;
using System.Linq;
using R3;

namespace StableCollections.Reactive.R3;

internal sealed class MutableReactiveStableArray2DImpl<T> : IMutableReactiveStableArray2D<T>, IMutableStableArray2D<T>, IStableArray2D<T>, IReactiveStableArray2D<T>, IObservableSource<IPersistentStableArray2D<T>>, IDisposable
{
	private readonly IMutableReactiveStableProperty<IPersistentStableArray2D<T>> _array;

	public int Width => ((IStableArray2D<T>)(object)_array.Value).Width;

	public int Height => ((IStableArray2D<T>)(object)_array.Value).Height;

	public T this[int x, int y]
	{
		get
		{
			return ((IStableArray2D<T>)(object)_array.Value)[x, y];
		}
		set
		{
			IPersistentStableArray2D<T> value2 = _array.Value;
			if (!EqualityComparer<T>.Default.Equals(((IStableArray2D<T>)(object)value2)[x, y], value))
			{
				_array.Value = value2.Set((x, y), value);
			}
		}
	}

	public MutableReactiveStableArray2DImpl(IEnumerable<T[]> elements)
	{
		_array = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableArray2D<T>>(StableCollectionsFactory.PersistentStableArray2DOf<T>(elements.ToArray()));
	}

	public MutableReactiveStableArray2DImpl(T[][] elements)
	{
		_array = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableArray2D<T>>(StableCollectionsFactory.PersistentStableArray2DOf<T>(elements));
	}

	public MutableReactiveStableArray2DImpl(ReadOnlySpan<T[]> elements)
	{
		_array = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableArray2D<T>>(StableCollectionsFactory.PersistentStableArray2DOf<T>(elements));
	}

	public bool Contains(T element)
	{
		return ((IStableArray2D<T>)(object)_array.Value).Contains(element);
	}

	public (int X, int Y) IndexOf(T element)
	{
		return ((IStableArray2D<T>)(object)_array.Value).IndexOf(element);
	}

	public Observable<IPersistentStableArray2D<T>> AsObservable()
	{
		return _array.AsObservable();
	}

	public void Dispose()
	{
		_array.Dispose();
	}

	public override string ToString()
	{
		return ((object)_array.Value).ToString();
	}
}

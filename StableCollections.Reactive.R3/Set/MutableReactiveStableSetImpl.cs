using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using R3;

namespace StableCollections.Reactive.R3;

internal sealed class MutableReactiveStableSetImpl<T> : IMutableReactiveStableSet<T>, IMutableStableSet<T>, IMutableStableCollection<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IStableSet<T>, IReactiveStableSet<T>, IObservableSource<IPersistentStableSet<T>>, IDisposable
{
	private readonly IMutableReactiveStableProperty<IPersistentStableSet<T>> _set;

	public int Size => ((IStableCollection<T>)(object)_set.Value).Size;

	public MutableReactiveStableSetImpl(IEnumerable<T> elements)
	{
		_set = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableSet<T>>(StableCollectionsFactory.PersistentStableSetOf<T>(elements.ToArray()));
	}

	public MutableReactiveStableSetImpl(T[] elements)
	{
		_set = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableSet<T>>(StableCollectionsFactory.PersistentStableSetOf<T>(elements));
	}

	public MutableReactiveStableSetImpl(ReadOnlySpan<T> elements)
	{
		_set = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableSet<T>>(StableCollectionsFactory.PersistentStableSetOf<T>(elements));
	}

	public IStableSet<T>.Enumerator GetEnumerator()
	{
		return ((IStableSet<T>)(object)_set.Value).GetEnumerator();
	}

	public bool IsEmpty()
	{
		return ((IStableCollection<T>)(object)_set.Value).IsEmpty();
	}

	public bool Contains(T element)
	{
		return ((IStableCollection<T>)(object)_set.Value).Contains(element);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return (IEnumerator<T>)(object)((IStableSet<T>)(object)_set.Value).GetEnumerator();
	}

	public bool Add(T element)
	{
		return Update((IPersistentStableSet<T> set) => set.Add(element));
	}

	public bool Remove(T element)
	{
		return Update((IPersistentStableSet<T> set) => set.Remove(element));
	}

	public bool AddAll(IStableCollection<T> elements)
	{
		return Update((IPersistentStableSet<T> set) => set.AddAll(elements));
	}

	public bool RemoveAll(IStableCollection<T> elements)
	{
		return Update((IPersistentStableSet<T> set) => set.RemoveAll(elements));
	}

	public bool RetainAll(IStableCollection<T> elements)
	{
		return Update((IPersistentStableSet<T> set) => set.RetainAll(elements));
	}

	public void Clear()
	{
		Update((IPersistentStableSet<T> set) => set.Clear());
	}

	public Observable<IPersistentStableSet<T>> AsObservable()
	{
		return _set.AsObservable();
	}

	public void Dispose()
	{
		_set.Dispose();
	}

	public override string ToString()
	{
		return ((object)_set.Value).ToString();
	}

	private bool Update(Func<IPersistentStableSet<T>, IPersistentStableSet<T>> update)
	{
		int size = ((IStableCollection<T>)(object)_set.Value).Size;
		_set.Value = update(_set.Value);
		return size != ((IStableCollection<T>)(object)_set.Value).Size;
	}
}

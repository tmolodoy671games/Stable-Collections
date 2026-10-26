using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using R3;

namespace StableCollections.Reactive.R3;

internal sealed class MutableReactiveStableListImpl<T> : IMutableReactiveStableList<T>, IMutableStableList<T>, IStableList<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IMutableStableCollection<T>, IReactiveStableList<T>, IObservableSource<IPersistentStableList<T>>, IDisposable
{
	private readonly IMutableReactiveStableProperty<IPersistentStableList<T>> _list;

	public int Size => ((IStableCollection<T>)(object)_list.Value).Size;

	public T this[int index]
	{
		get
		{
			return ((IStableList<T>)(object)_list.Value)[index];
		}
		set
		{
			IPersistentStableList<T> value2 = _list.Value;
			if (!EqualityComparer<T>.Default.Equals(((IStableList<T>)(object)value2)[index], value))
			{
				_list.Value = value2.Set(index, value);
			}
		}
	}

	public MutableReactiveStableListImpl(IEnumerable<T> elements)
	{
		_list = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableList<T>>(StableCollectionsFactory.PersistentStableListOf<T>(elements.ToArray()));
	}

	public MutableReactiveStableListImpl(T[] elements)
	{
		_list = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableList<T>>(StableCollectionsFactory.PersistentStableListOf<T>(elements));
	}

	public MutableReactiveStableListImpl(ReadOnlySpan<T> elements)
	{
		_list = ReactiveStableCollectionsFactory.MutableReactiveStablePropertyOf<IPersistentStableList<T>>(StableCollectionsFactory.PersistentStableListOf<T>(elements));
	}

	public bool IsEmpty()
	{
		return ((IStableCollection<T>)(object)_list.Value).IsEmpty();
	}

	public bool Contains(T element)
	{
		return ((IStableCollection<T>)(object)_list.Value).Contains(element);
	}

	IStableList<T>.Enumerator IStableList<T>.GetEnumerator()
	{
		return ((IStableList<T>)(object)_list.Value).GetEnumerator();
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return (IEnumerator<T>)(object)((IStableList<T>)(object)_list.Value).GetEnumerator();
	}

	public int IndexOf(T element)
	{
		return ((IStableList<T>)(object)_list.Value).IndexOf(element);
	}

	public int LastIndexOf(T element)
	{
		return ((IStableList<T>)(object)_list.Value).LastIndexOf(element);
	}

	public IStableList<T> SubList(int fromIndex, int toIndex)
	{
		return ((IStableList<T>)(object)_list.Value).SubList(fromIndex, toIndex);
	}

	public void Add(int index, T element)
	{
		Update((IPersistentStableList<T> list) => list.Add(index, element));
	}

	public void RemoveAt(int index)
	{
		Update((IPersistentStableList<T> list) => list.RemoveAt(index));
	}

	public bool Add(T element)
	{
		return Update((IPersistentStableList<T> list) => list.Add(element));
	}

	public bool Remove(T element)
	{
		return Update((IPersistentStableList<T> list) => list.Remove(element));
	}

	public bool AddAll(IStableCollection<T> elements)
	{
		return Update((IPersistentStableList<T> list) => list.AddAll(elements));
	}

	public bool RemoveAll(IStableCollection<T> elements)
	{
		return Update((IPersistentStableList<T> list) => list.RemoveAll(elements));
	}

	public bool RetainAll(IStableCollection<T> elements)
	{
		return Update((IPersistentStableList<T> list) => list.RetainAll(elements));
	}

	public void Clear()
	{
		Update((IPersistentStableList<T> list) => list.Clear());
	}

	public Observable<IPersistentStableList<T>> AsObservable()
	{
		return _list.AsObservable();
	}

	public void Dispose()
	{
		_list.Dispose();
	}

	public override string ToString()
	{
		return ((object)_list.Value).ToString();
	}

	private bool Update(Func<IPersistentStableList<T>, IPersistentStableList<T>> update)
	{
		int size = ((IStableCollection<T>)(object)_list.Value).Size;
		_list.Value = update(_list.Value);
		return size != ((IStableCollection<T>)(object)_list.Value).Size;
	}
}

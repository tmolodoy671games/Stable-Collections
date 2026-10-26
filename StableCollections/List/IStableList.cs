using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IStableList<T> : IStableCollection<T>, IEnumerable<T>, IEnumerable
{
	public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
	{
		private readonly List<T> _list;

		private List<T>.Enumerator _listEnumerator;

		public T Current => _listEnumerator.Current;

		object? IEnumerator.Current => Current;

		internal Enumerator(List<T> list)
		{
			_list = list;
			_listEnumerator = list.GetEnumerator();
		}

		public bool MoveNext()
		{
			return _listEnumerator.MoveNext();
		}

		public void Reset()
		{
			_listEnumerator.Dispose();
			_listEnumerator = _list.GetEnumerator();
		}

		public void Dispose()
		{
			_listEnumerator.Dispose();
		}
	}

	int LastIndex => Size - 1;

	IEnumerable<int> Indices
	{
		get
		{
			for (int i = 0; i <= LastIndex; i++)
			{
				yield return i;
			}
		}
	}

	T this[int index] { get; }

	int IndexOf(T element);

	int LastIndexOf(T element);

	IStableList<T> SubList(int fromIndex, int toIndex);

	new Enumerator GetEnumerator();
}

using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IStableSet<T> : IStableCollection<T>, IEnumerable<T>, IEnumerable
{
	public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
	{
		private readonly HashSet<T> _set;

		private HashSet<T>.Enumerator _setEnumerator;

		public T Current => _setEnumerator.Current;

		object? IEnumerator.Current => Current;

		internal Enumerator(HashSet<T> set)
		{
			_set = set;
			_setEnumerator = set.GetEnumerator();
		}

		public bool MoveNext()
		{
			return _setEnumerator.MoveNext();
		}

		public void Reset()
		{
			_setEnumerator.Dispose();
			_setEnumerator = _set.GetEnumerator();
		}

		public void Dispose()
		{
			_setEnumerator.Dispose();
		}
	}

	new Enumerator GetEnumerator();
}

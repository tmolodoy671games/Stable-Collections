using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IStableCollection<T> : IEnumerable<T>, IEnumerable
{
	int Size { get; }

	bool IsEmpty();

	bool Contains(T element);

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

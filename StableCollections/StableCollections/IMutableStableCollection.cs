using System.Collections;
using System.Collections.Generic;

namespace StableCollections;

public interface IMutableStableCollection<T> : IStableCollection<T>, IEnumerable<T>, IEnumerable
{
	bool Add(T element);

	bool Remove(T element);

	bool AddAll(IStableCollection<T> elements);

	bool RemoveAll(IStableCollection<T> elements);

	bool RetainAll(IStableCollection<T> elements);

	void Clear();

	public void operator +=(T element)
	{
		Add(element);
	}

	public void operator +=(IEnumerable<T> elements)
	{
		this.AddAll(elements);
	}
}


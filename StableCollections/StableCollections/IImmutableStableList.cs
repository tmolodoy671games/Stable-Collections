using System.Collections;
using System.Collections.Generic;

namespace StableCollections;

public interface IImmutableStableList<T> : IStableList<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IImmutableStableCollection<T>
{
}

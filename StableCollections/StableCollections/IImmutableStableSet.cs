using System.Collections;
using System.Collections.Generic;

namespace StableCollections;

public interface IImmutableStableSet<T> : IStableSet<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable
{
}

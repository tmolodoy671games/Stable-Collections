using System.Collections;
using System.Collections.Generic;

namespace StableCollections;

public interface IImmutableStableCollection<T> : IStableCollection<T>, IEnumerable<T>, IEnumerable
{
}

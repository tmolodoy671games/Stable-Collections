using System.Collections;
using System.Collections.Generic;

namespace StableCollections;

public interface IPersistentStableCollection<T> : IImmutableStableCollection<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable
{
}

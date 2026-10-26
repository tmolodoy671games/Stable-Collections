using System.Collections;
using System.Collections.Generic;

namespace StableCollections;

public interface IImmutableStableMap<TKey, TValue> : IStableMap<TKey, TValue>, IEnumerable<StableMapEntry<TKey, TValue>>, IEnumerable
{
}

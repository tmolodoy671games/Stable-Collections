using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IImmutableStableMap<TKey, TValue> : IStableMap<TKey, TValue>, IEnumerable<StableMapEntry<TKey, TValue>>, IEnumerable
{
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace StableCollections.Reactive.R3;

public interface IReactiveStableMap<TKey, TValue> : IStableMap<TKey, TValue>, IEnumerable<StableMapEntry<TKey, TValue>>, IEnumerable, IObservableSource<IPersistentStableMap<TKey, TValue>>, IDisposable
{
}

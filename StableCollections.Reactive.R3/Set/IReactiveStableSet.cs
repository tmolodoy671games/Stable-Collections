using System;
using System.Collections;
using System.Collections.Generic;

namespace StableCollections.Reactive.R3;

public interface IReactiveStableSet<T> : IStableSet<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IObservableSource<IPersistentStableSet<T>>, IDisposable
{
}

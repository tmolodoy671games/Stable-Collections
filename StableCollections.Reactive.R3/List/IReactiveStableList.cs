using System;
using System.Collections;
using System.Collections.Generic;

namespace StableCollections.Reactive.R3;

public interface IReactiveStableList<T> : IStableList<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IObservableSource<IPersistentStableList<T>>, IDisposable
{
}

using System;

namespace StableCollections.Reactive.R3;

public interface IReactiveStableArray2D<T> : IStableArray2D<T>, IObservableSource<IPersistentStableArray2D<T>>, IDisposable
{
}

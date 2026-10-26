using System;

namespace StableCollections.Reactive.R3;

public interface IReactiveStableProperty<T> : IObservableSource<T>, IDisposable
{
	T Value { get; }
}

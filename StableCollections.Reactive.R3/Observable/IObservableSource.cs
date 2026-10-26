using System;
using R3;

namespace StableCollections.Reactive.R3;

public interface IObservableSource<T> : IDisposable
{
	Observable<T> AsObservable();
}

using System;

namespace StableCollections.Reactive.R3;

public interface IMutableReactiveStableProperty
{
	static IMutableReactiveStableProperty<T> Create<T>(T initialValue)
	{
		return new MutableReactiveStablePropertyImpl<T>(initialValue);
	}
}
public interface IMutableReactiveStableProperty<T> : IReactiveStableProperty<T>, IObservableSource<T>, IDisposable
{
	new T Value { get; set; }
}

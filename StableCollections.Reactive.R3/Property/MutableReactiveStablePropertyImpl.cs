using System;
using System.Collections.Generic;
using R3;

namespace StableCollections.Reactive.R3;

internal sealed class MutableReactiveStablePropertyImpl<T>(T initialValue) : IMutableReactiveStableProperty<T>, IReactiveStableProperty<T>, IObservableSource<T>, IDisposable
{
	private readonly BehaviorSubject<T> _subject = new BehaviorSubject<T>(initialValue);

	private T _value = initialValue;

	private bool _isDisposed;

	public T Value
	{
		get
		{
			return _value;
		}
		set
		{
			if (_isDisposed)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
			if (!EqualityComparer<T>.Default.Equals(_value, value))
			{
				_value = value;
				_subject.OnNext(value);
			}
		}
	}

	public Observable<T> AsObservable()
	{
		return ObservableExtensions.AsObservable<T>((Observable<T>)(object)_subject);
	}

	public void Dispose()
	{
		if (!_isDisposed)
		{
			_isDisposed = true;
			SubjectExtensions.OnCompleted<T>((ISubject<T>)(object)_subject);
			_subject.Dispose();
		}
	}

	public override string ToString()
	{
		T value = Value;
		return ((value != null) ? value.ToString() : null) ?? "null";
	}
}

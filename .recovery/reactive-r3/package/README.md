# Reactive Stable Collections

Reactive extension of Stable Collections package.

## Usage

```csharp
using R3;
using StableCollections.Reactive.R3;
using static StableCollections.Reactive.R3.ReactiveStableCollectionsFactory;

using var list = MutableReactiveStableListOf(1, 2, 3);

// Emits the current snapshot immediately, then a new snapshot after every change.
list.AsObservable().Subscribe(snapshot => Console.WriteLine(snapshot));

list.Add(4);     // [1, 2, 3, 4]
list.Remove(42); // nothing changed, nothing emitted
```

- `AsObservable()` emits immutable snapshots (`IPersistentStable*`), starting with the current one.
- No notification is emitted when an operation does not change the collection.
- Collections are not thread-safe: like R3 itself, they do no synchronization.
- Collections are `IDisposable`: `Dispose()` completes the observable, and later mutations throw `ObjectDisposedException`.
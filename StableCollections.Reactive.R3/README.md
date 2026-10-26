# Stable Collections Reactive R3

Reactive collections for R3, targeting .NET Standard 2.1.

## Installation

```powershell
dotnet add package T.Molodoy.StableCollections.Reactive.R3 --version 0.0.45
```

## Usage

```csharp
using R3;
using StableCollections.Reactive.R3;
using static StableCollections.Reactive.R3.ReactiveStableCollectionsFactory;

using var list = MutableReactiveStableListOf(1, 2, 3);
using var subscription = list.AsObservable().Subscribe(snapshot => Console.WriteLine(snapshot));
list.Add(4);
```

Observables emit the current immutable snapshot immediately and emit updates when the collection changes. Dispose collections when finished to complete subscriptions. Collections do not synchronize concurrent access.

The package depends on R3 1.3.0 and T.Molodoy.StableCollections 0.0.45.
The assembly name is `Reactive Stable Collections`; namespaces use `StableCollections.Reactive.R3`.

## Build

Requires the .NET 10 SDK and C# 14.

```powershell
dotnet pack StableCollections.Reactive.R3/StableCollections.Reactive.R3.csproj -c Release -o artifacts -m:1
```

Licensed under the MIT license.

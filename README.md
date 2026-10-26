# Stable Collections

Java-like collections for C#: mutable and persistent lists, sets, maps and two-dimensional arrays.

## Installation

```powershell
dotnet add package T.Molodoy.StableCollections --version 0.0.45
```

Targets .NET Standard 2.1. C# 14 is required to build the source project.

## Usage

```csharp
using static StableCollections.StableCollectionsFactory;

var numbers = MutableStableListOf(1, 2, 3);
var snapshot = PersistentStableListOf(1, 2, 3);
```

## Build package

```powershell
dotnet pack StableCollections/StableCollections.csproj -c Release -o artifacts
```

Licensed under the MIT license.

## Reactive collections (R3)

The separate `StableCollections.Reactive.R3` project provides reactive lists, sets,
maps, two-dimensional arrays and properties. Sources are grouped by collection type.

```powershell
dotnet add package T.Molodoy.StableCollections.Reactive.R3 --version 0.0.45
dotnet pack StableCollections.Reactive.R3/StableCollections.Reactive.R3.csproj -c Release -o artifacts -m:1
```

See [the reactive library README](StableCollections.Reactive.R3/README.md) for usage.

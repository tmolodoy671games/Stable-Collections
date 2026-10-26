using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IPersistentStableCollection<T> : IImmutableStableCollection<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable
{
}

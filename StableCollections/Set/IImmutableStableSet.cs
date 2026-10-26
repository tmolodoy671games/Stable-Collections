using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IImmutableStableSet<T> : IStableSet<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable
{
}

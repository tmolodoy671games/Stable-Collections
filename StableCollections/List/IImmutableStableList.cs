using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IImmutableStableList<T> : IStableList<T>, IStableCollection<T>, IEnumerable<T>, IEnumerable, IImmutableStableCollection<T>
{
}

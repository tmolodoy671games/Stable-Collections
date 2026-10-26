// ReSharper disable once CheckNamespace
namespace StableCollections;

public readonly record struct StableMapEntry<TKey, TValue>(TKey Key, TValue Value)
{
	public override string ToString()
	{
		return $"({Key}, {Value})";
	}
}

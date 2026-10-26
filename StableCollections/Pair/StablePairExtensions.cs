// ReSharper disable once CheckNamespace
namespace StableCollections;

public static class StablePairExtensions
{
	public static (A First, B Second) To<A, B>(this A first, B second)
	{
		return (First: first, Second: second);
	}
}

// ReSharper disable once CheckNamespace
namespace StableCollections;

public static class StablePairExtensions
{
	public static (TA First, TB Second) To<TA, TB>(this TA first, TB second)
	{
		return (First: first, Second: second);
	}
}

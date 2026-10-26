// ReSharper disable once CheckNamespace
namespace StableCollections;

public interface IStableArray2D<T>
{
	int Width { get; }

	int Height { get; }

	T this[int x, int y] { get; }

	bool Contains(T element);

	(int X, int Y) IndexOf(T element);
}

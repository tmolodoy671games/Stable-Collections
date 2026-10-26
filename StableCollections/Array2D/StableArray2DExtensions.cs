using System;
using StableCollections.Array2D.Persistent;

// ReSharper disable once CheckNamespace
namespace StableCollections;

public static class StableArray2DExtensions
{
	public static IPersistentStableArray2D<T2> Select<T1, T2>(this IStableArray2D<T1> array, Func<T1, T2> selector)
	{
		T2[,] array2 = new T2[array.Height, array.Width];
		for (int i = 0; i < array.Width; i++)
		{
			for (int j = 0; j < array.Height; j++)
			{
				array2[j, i] = selector(array[i, j]);
			}
		}
		return new PersistentStableArray2DImpl<T2>(array2);
	}
}

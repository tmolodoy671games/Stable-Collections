using System;
using System.Collections.Generic;

namespace StableCollections.Array2D.Mutable;

internal class MutableStableArray2DImpl<T> : IMutableStableArray2D<T>, IStableArray2D<T>
{
	private readonly T[,] _array;

	public int Width => _array.GetLength(1);

	public int Height => _array.GetLength(0);

	public T this[int x, int y]
	{
		get
		{
			return _array[y, x];
		}
		set
		{
			_array[y, x] = value;
		}
	}

	public MutableStableArray2DImpl(T[,] array)
	{
		_array = (T[,])array.Clone();
	}

	public MutableStableArray2DImpl(ReadOnlySpan<T[]> rows)
	{
		if (rows.Length == 0)
		{
			_array = new T[0, 0];
			return;
		}
		int num = rows[0].Length;
		_array = new T[rows.Length, num];
		for (int i = 0; i < rows.Length; i++)
		{
			if (rows[i].Length != num)
			{
				throw new ArgumentException("All rows must have the same length.", "rows");
			}
			for (int j = 0; j < num; j++)
			{
				_array[i, j] = rows[i][j];
			}
		}
	}

	public (int X, int Y) IndexOf(T element)
	{
		for (int i = 0; i < Height; i++)
		{
			for (int j = 0; j < Width; j++)
			{
				if (EqualityComparer<T>.Default.Equals(this[j, i], element))
				{
					return (X: j, Y: i);
				}
			}
		}
		return (X: -1, Y: -1);
	}

	public bool Contains(T element)
	{
		T[,] array = _array;
		foreach (T x in array)
		{
			if (EqualityComparer<T>.Default.Equals(x, element))
			{
				return true;
			}
		}
		return false;
	}

	public override int GetHashCode()
	{
		HashCode hashCode = default;
		hashCode.Add(Width);
		hashCode.Add(Height);
		T[,] array = _array;
		foreach (T value in array)
		{
			hashCode.Add(value);
		}
		return hashCode.ToHashCode();
	}

	public override bool Equals(object? obj)
	{
		if (this == obj)
		{
			return true;
		}
		if (!(obj is IStableArray2D<T> stableArray2D))
		{
			return false;
		}
		if (Width != stableArray2D.Width || Height != stableArray2D.Height)
		{
			return false;
		}
		EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
		for (int i = 0; i < Height; i++)
		{
			for (int j = 0; j < Width; j++)
			{
				if (!equalityComparer.Equals(this[j, i], stableArray2D[j, i]))
				{
					return false;
				}
			}
		}
		return true;
	}
}

using System;

namespace StableCollections;

internal static class KotlinRandom
{
	[ThreadStatic]
	private static Random? _instance;

	public static Random Instance => _instance ?? (_instance = new Random());
}

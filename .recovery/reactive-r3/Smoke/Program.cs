using System;
using System.Collections.Generic;
using R3;
using static StableCollections.Reactive.R3.ReactiveStableCollectionsFactory;

static void Check(bool condition) { if (!condition) throw new Exception("Smoke check failed"); }
using var list = MutableReactiveStableListOf(1, 2, 3);
var sizes = new List<int>();
using var subscription = list.AsObservable().Subscribe(snapshot => sizes.Add(snapshot.Size));
list.Add(4);
list.Remove(42);
Check(sizes.Count == 2 && sizes[0] == 3 && sizes[1] == 4);
Check(string.Join(",", list) == "1,2,3,4");
using var set = MutableReactiveStableSetOf(1, 2);
set.Add(2);
Check(set.Size == 2 && new List<int>(set).Count == 2);
using var map = MutableReactiveStableMapOf(("a", 1));
map["b"] = 2;
Check(map.Size == 2 && map["b"] == 2);
using var array = MutableReactiveStableArray2DOf(new[] { 1, 2 });
array[0, 0] = 9;
Check(array[0, 0] == 9);
using var property = MutableReactiveStablePropertyOf(1);
int emissions = 0;
bool completed = false;
using var ps = property.AsObservable().Subscribe(_ => emissions++, _ => completed = true);
property.Value = 1;
property.Value = 2;
Check(emissions == 2);
property.Dispose();
Check(completed);
try { property.Value = 3; throw new Exception("Expected disposal exception"); }
catch (ObjectDisposedException) { }
Console.WriteLine("Reactive R3 smoke checks passed.");

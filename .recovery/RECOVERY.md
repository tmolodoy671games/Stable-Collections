# Stable Collections

Recovered on 2026-10-26 from the locally cached T.Molodoy.StableCollections 0.0.44 package used by Compose.Net.

The C# sources were decompiled with ILSpy 11.1.0.9782. The original Git history, tests, comments and formatting were not recovered. The package records original commit 652302431042b32737657bae372ac24b962fd351; its repository URL was unavailable during recovery.

Two compound assignment operators were restored to C# 14 syntax after decompilation. Build with the .NET 10 SDK:

```powershell
dotnet build StableCollections/StableCollections.csproj
```

Compose.Net, Compose.Net.Navigation and Compose.Net CodeGen Sample reference this project directly. Keep the Stable Collections and Compose.Net folders next to each other.

The original package is preserved in .recovery/original for comparison and future recovery. The MIT license is preserved in LICENSE.txt.

Validation: StableCollections and Compose.Net build successfully. Compose.Net.Tests: 183 passed, 8 skipped with DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1. Under the machine culture, Shapes_are_recorded fails because it expects decimal points rather than commas.

# dagver-nuget

Versioning for Git Commit DAG (Directed acyclic graph).

[![NuGet](https://img.shields.io/nuget/v/DagVer.svg)](https://www.nuget.org/packages/DagVer/)

It works for C# projects on Windows and Linux.

Current version formats:

- `PackageVersion` = `FileVersion` = `{Major}.{Minor}.{Height}.{Commit16}`
- `AssemblyVersion` = `{Major}.0.0.0`

where

- `{Major}` is a major part of a user's defined version number.
- `{Major}` is a minor part of a user's defined version number.
- `{Height}` is a number of commits in the longest path.
- `{Commit16}` is a first 16 bits of a commit id.

See also [DagVer for Node.js](https://github.com/sergey-shandar/dagver) and [GitVersioning](https://github.com/AArnott/Nerdbank.GitVersioning).

## Known Issues

1. Add `PrivateAssets="All"` to your project file. For example,
   ```xml
   <PackageReference Include="DagVer" Version="0.1.0.47" PrivateAssets="All" />
   ```
1. The tool can't change package version for multi target projects. For example, if your project file contains
   ```xml
   <TargetFrameworks>netstandard1.4;net452</TargetFrameworks>
   ```

## For Developers

[![Build status](https://ci.appveyor.com/api/projects/status/q4b0u1lsdj7xt1wt?svg=true)](https://ci.appveyor.com/project/sergey-shandar/dagver-nuget)

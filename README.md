# dagver-nuget

Versioning for Git Commit DAG (Directed acyclic graph).

It works for C# projects on Windows and Linux.

Current version formats:

- `PackageSersion` = `FileVersion` = `{Major}.{MergeMax}.{Local}`
- `AssemblyVersion` = `{Major}.0.0.0`

where

- `{Major}` is a major part of user's defined version number.
- `{MergeMax}` is a number of mergers in a Git revision path which contains the maximum number of merges.
- `{Local}` is a number of commits from the last merge.

See also [DagVer for Node.js](https://github.com/sergey-shandar/dagver) and [GitVersioning](https://github.com/AArnott/Nerdbank.GitVersioning).

## For Devlopers

[![Build status](https://ci.appveyor.com/api/projects/status/q4b0u1lsdj7xt1wt?svg=true)](https://ci.appveyor.com/project/sergey-shandar/dagver-nuget)

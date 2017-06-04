$nugetExe = Join-Path (pwd) "nuget.exe"

$client = new-object System.Net.WebClient
$client.DownloadFile("https://dist.nuget.org/win-x86-commandline/v4.1.0/nuget.exe", $nugetExe)

$version = if ($env:APPVEYOR_BUILD_NUMBER) {
    "0.0.0." + $env:APPVEYOR_BUILD_NUMBER
} else {
    "0-unknown"
}

dotnet restore program/
dotnet build program/

.\nuget.exe pack DagVer.nuspec -Version $version

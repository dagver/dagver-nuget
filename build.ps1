$nugetExe = Join-Path (pwd) "nuget.exe"

$client = new-object System.Net.WebClient
$client.DownloadFile("https://dist.nuget.org/win-x86-commandline/v4.1.0/nuget.exe", $nugetExe)

.\nuget.exe pack DagVer.nuspec
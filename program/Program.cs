using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace program
{
    class Program
    {
        public static string[] GetRevList()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "rev-list --parents HEAD",
                RedirectStandardOutput = true,
            };
            using (var process = new Process { StartInfo = startInfo })
            {
                process.Start();
                return process.StandardOutput
                    .ReadToEnd()
                    .Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            }
        }

        public static IEnumerable<T> Reversed<T>(ICollection<T> v)
        {
            for(var i = v.Count(); i > 0;)
            {
                --i;
                yield return v.ElementAt(i);
            }
        }

        public static Version GetVersion()
        {
            var map = new Dictionary<string, Version>();

            var revList = GetRevList();

            Version version = null;

            // add all lines to map
            foreach (var rev in Reversed(revList))
            {
                var commit = rev.Split(' ');
                var c = commit[0];
                switch (commit.Length)
                {
                    case 1:
                        version = new Version(0, 0, 0, c);
                        break;
                    case 2:
                        var v = map[commit[1]];
                        version = new Version(
                            v.Height + 1, v.Merge, v.Local + 1, c);
                        break;
                    default:
                        int h = 0;
                        int m = 0;
                        foreach(var p in commit.Skip(1))
                        {
                            v = map[p];
                            h = Math.Max(h, v.Height);
                            m = Math.Max(m, v.Merge);
                        }
                        version = new Version(h + 1, m + 1, 0, c);
                        break;
                }
                map[c] = version;
            }

            return version;
        }

        public sealed class Version
        {
            public int Height { get; }
            public int Merge { get; }
            public int Local { get; }

            public string Commit { get; }

            public int Commit16
                => int.Parse(Commit.Substring(0, 4), NumberStyles.HexNumber);

            public Version(int height, int merge, int local, string commit)
            {
                Height = height;
                Merge = merge;
                Local = local;
                Commit = commit;
            }

            public override string ToString()
                => $"{Height}.{Merge}.{Local}.{Commit16}";
        }

        static void Main(string[] args)
        {
            var major = args.Length > 1 ? args[1].Split('.')[0] : "0";
            var v = GetVersion();
            Console.WriteLine($"{major}.{v.Merge}.{v.Local}.{v.Commit16}");
        }
    }
}
